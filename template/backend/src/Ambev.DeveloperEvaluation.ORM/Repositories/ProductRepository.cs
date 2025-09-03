using Ambev.DeveloperEvaluation.Common.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly DefaultContext _context;

    public ProductRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default)
    {
        await _context.Products.AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return product;
    }

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<Product?> UpdateAsync(Product product, CancellationToken cancellationToken = default)
    {
        var existing = await _context.Products.FirstOrDefaultAsync(p => p.Id == product.Id, cancellationToken);
        if (existing == null) return null;

        _context.Entry(existing).CurrentValues.SetValues(product);
        await _context.SaveChangesAsync(cancellationToken);

        return existing;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await GetByIdAsync(id, cancellationToken);
        if (product == null)
            return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<IReadOnlyList<string>> GetCategoriesAsync(CancellationToken ct = default)
    {
        return await _context.Products
            .AsNoTracking()
            .Where(p => !string.IsNullOrEmpty(p.Category))
            .Select(p => p.Category!)
            .Distinct()
            .OrderBy(c => c)
            .ToListAsync(ct);
    }

    public async Task<PagedResult<Product>> ListAsync(
        int page,
        int size,
        string? order,
        CancellationToken ct = default)
    {
        var query = _context.Products.AsNoTracking();

        // Ordenação simples: aceita "id", "title", "price", "category" (+ "asc"/"desc")
        query = ApplyOrdering(query, order);

        var totalItems = await query.CountAsync(ct);
        var totalPages = Math.Max(1, (int)Math.Ceiling(totalItems / (double)size));
        var currentPage = Math.Min(Math.Max(1, page), totalPages);

        var data = await query
            .Skip((currentPage - 1) * size)
            .Take(size)
            .ToListAsync(ct);

        return new PagedResult<Product>
        {
            Data = data,
            TotalItems = totalItems,
            CurrentPage = currentPage,
            TotalPages = totalPages
        };
    }

    public async Task<PagedResult<Product>> ListByCategoryAsync(
        string category,
        int page,
        int size,
        string? order,
        CancellationToken ct = default)
    {
        // defesa básica
        if (string.IsNullOrWhiteSpace(category))
        {
            return new PagedResult<Product>
            {
                Data = Enumerable.Empty<Product>(),
                TotalItems = 0,
                CurrentPage = 1,
                TotalPages = 1
            };
        }

        // filtro por categoria
        var query = _context.Products
            .AsNoTracking()
            .Where(p => p.Category!.ToLower() == category.ToLower());

        // ordenação
        query = ApplyOrdering(query, order);

        // paginação
        var totalItems = await query.CountAsync(ct);
        var totalPages = Math.Max(1, (int)Math.Ceiling(totalItems / (double)size));
        var currentPage = Math.Min(Math.Max(1, page), totalPages);

        var data = await query
            .Skip((currentPage - 1) * size)
            .Take(size)
            .ToListAsync(ct);

        return new PagedResult<Product>
        {
            Data = data,
            TotalItems = totalItems,
            CurrentPage = currentPage,
            TotalPages = totalPages
        };
    }


    // --- Helper de ordenação minimalista ---
    private static IQueryable<Product> ApplyOrdering(IQueryable<Product> q, string? order)
    {
        if (string.IsNullOrWhiteSpace(order))
            return q.OrderBy(p => p.Id);

        IOrderedQueryable<Product>? ordered = null;

        foreach (var raw in order.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
        {
            var parts = raw.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var field = parts[0].ToLowerInvariant();
            var desc = parts.Length > 1 && parts[1].Equals("desc", StringComparison.OrdinalIgnoreCase);

            Expression<Func<Product, object>> key = field switch
            {
                "id" => p => p.Id,
                "title" => p => p.Title!,
                "price" => p => p.Price,
                "category" => p => p.Category!,
                _ => p => p.Id
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

        return ordered ?? q.OrderBy(p => p.Id);
    }
}
