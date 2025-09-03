using Ambev.DeveloperEvaluation.Common.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly DefaultContext _context;

    public SaleRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        await _context.Sales.AddAsync(sale, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return sale;
    }

    public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Sales
            .Include(s => s.Items)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<Sale?> UpdateAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        var existing = await _context.Sales
            .Include(s => s.Items)
            .FirstOrDefaultAsync(s => s.Id == sale.Id, cancellationToken);

        if (existing == null) return null;

        // Atualiza cabeçalho
        existing.Date = sale.Date;
        existing.CustomerId = sale.CustomerId;
        existing.CustomerName = sale.CustomerName;
        existing.CustomerEmail = sale.CustomerEmail;
        existing.CustomerPhone = sale.CustomerPhone;
        existing.Branch = sale.Branch;

        // Remove os que não vieram no update
        var itemsToRemove = existing.Items
            .Where(ei => sale.Items.All(ni => ei.Id != Guid.Empty && ni.Id != ei.Id))
            .ToList();

        foreach (var item in itemsToRemove)
        {
            existing.Items.Remove(item);
        }

        foreach (var item in sale.Items)
        {

            var existingItem = existing.Items.FirstOrDefault(ei => item.Id != Guid.Empty && ei.Id == item.Id);

            if (existingItem != null)
            {
                // Atualiza campo a campo
                existingItem.ProductId = item.ProductId;
                existingItem.ProductTitle = item.ProductTitle;
                existingItem.ProductCategory = item.ProductCategory;
                existingItem.Quantity = item.Quantity;
                existingItem.UnitPrice = item.UnitPrice;
                existingItem.Discount = item.Discount;
                existingItem.TotalAmount = item.TotalAmount;
            }
            else
            {
                // Insere novo
                var newItem = new SaleItem
                {
                    Id = Guid.Empty,
                    SaleId = existing.Id,
                    ProductId = item.ProductId,
                    ProductTitle = item.ProductTitle,
                    ProductCategory = item.ProductCategory,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Discount = item.Discount,
                    TotalAmount = item.TotalAmount
                };

                existing.Items.Add(newItem);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);
        return existing;
    }


    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var sale = await GetByIdAsync(id, cancellationToken);
        if (sale == null) return false;

        _context.Sales.Remove(sale);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<PagedResult<Sale>> ListAsync(
        int page = 1,
        int size = 10,
        string? order = null,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Sales.Include(s => s.Items).AsQueryable();

        query = ApplyOrdering(query, order);

        var totalItems = await query.CountAsync(cancellationToken);
        var data = await query
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync(cancellationToken);

        return new PagedResult<Sale>
        {
            Data = data,
            TotalItems = totalItems,
            CurrentPage = page,
            TotalPages = (int)Math.Ceiling(totalItems / (double)size)
        };
    }

    private static IQueryable<Sale> ApplyOrdering(IQueryable<Sale> q, string? order)
    {
        if (string.IsNullOrWhiteSpace(order))
            return q.OrderBy(s => s.Date);

        IOrderedQueryable<Sale>? ordered = null;

        foreach (var raw in order.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
        {
            var parts = raw.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var field = parts[0].ToLowerInvariant();
            var desc = parts.Length > 1 && parts[1].Equals("desc", StringComparison.OrdinalIgnoreCase);

            Expression<Func<Sale, object>> key = field switch
            {
                "id" => s => s.Id,
                "date" => s => s.Date,
                "customerid" => s => s.CustomerId,
                "customername" => s => s.CustomerName,
                "branch" => s => s.Branch,
                "totalamount" => s => s.TotalAmount,
                _ => s => s.Date
            };

            if (ordered == null)
            {
                ordered = desc ? q.OrderByDescending(key) : q.OrderBy(key);
            }
            else
            {
                ordered = desc ? ordered.ThenByDescending(key) : ordered.ThenBy(key);
            }
        }

        return ordered ?? q.OrderBy(s => s.Date);
    }
}
