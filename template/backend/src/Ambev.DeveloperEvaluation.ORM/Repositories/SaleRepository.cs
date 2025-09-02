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
        var existing = await GetByIdAsync(sale.Id, cancellationToken);
        if (existing == null) return null;

        _context.Entry(existing).CurrentValues.SetValues(sale);

        existing.Items.Clear();
        foreach (var item in sale.Items)
            existing.Items.Add(item);

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
                "salenumber" => s => s.SaleNumber,
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
