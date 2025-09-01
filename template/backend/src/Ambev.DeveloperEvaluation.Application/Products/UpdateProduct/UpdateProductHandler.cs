using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, UpdateProductResult?>
{
    private readonly IProductRepository _repo;

    public UpdateProductHandler(IProductRepository repo) => _repo = repo;

    public async Task<UpdateProductResult?> Handle(UpdateProductCommand r, CancellationToken ct)
    {
        var existing = await _repo.GetByIdAsync(r.Id, ct);
        if (existing is null) return null;

        existing.Title = r.Title;
        existing.Price = r.Price;
        existing.Description = r.Description;
        existing.Category = r.Category;
        existing.Image = r.Image;
        existing.RatingRate = r.RatingRate;
        existing.RatingCount = r.RatingCount;

        var saved = await _repo.UpdateAsync(existing, ct);
        if (saved is null) return null;

        return new UpdateProductResult
        {
            Id = saved.Id,
            Title = saved.Title,
            Price = saved.Price,
            Description = saved.Description,
            Category = saved.Category,
            Image = saved.Image,
            Rating = new UpdateProductResult.RatingResult
            {
                Rate = saved.RatingRate,
                Count = saved.RatingCount
            }
        };
    }
}
