using Ambev.DeveloperEvaluation.Common.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository contract for managing Product entities
/// </summary>
public interface IProductRepository
{
    /// <summary>
    /// Creates a new product
    /// </summary>
    Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a product by its unique identifier
    /// </summary>
    Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing product
    /// </summary>
    Task<Product?> UpdateAsync(Product product, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a product by id
    /// </summary>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// List a category for products
    /// </summary>
    Task<IReadOnlyList<string>> GetCategoriesAsync(CancellationToken ct = default);

    /// <summary>
    /// Lists all products with pagination and ordering
    /// </summary>
    Task<PagedResult<Product>> ListAsync(
        int page,
        int size,
        string? order,
        CancellationToken ct = default);

    /// <summary>
    /// Lists products by category with pagination and ordering
    /// </summary>
    Task<PagedResult<Product>> ListByCategoryAsync(
        string category,
        int page,
        int size,
        string? order,
        CancellationToken ct = default);
}
