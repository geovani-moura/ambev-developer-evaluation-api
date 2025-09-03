using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, DeleteProductResult>
{
    private readonly IProductRepository _repo;
    public DeleteProductHandler(IProductRepository repo) => _repo = repo;

    public async Task<DeleteProductResult> Handle(DeleteProductCommand r, CancellationToken cancellationToken)
    {
        var ok = await _repo.DeleteAsync(r.Id, cancellationToken);
        return new DeleteProductResult { Success = ok };
    }
}
