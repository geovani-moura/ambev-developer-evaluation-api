using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, DeleteSaleResult>
{
    private readonly ISaleRepository _repo;
    public DeleteSaleHandler(ISaleRepository repo) => _repo = repo;

    public async Task<DeleteSaleResult> Handle(DeleteSaleCommand r, CancellationToken ct)
    {
        var ok = await _repo.DeleteAsync(r.Id, ct);
        return new DeleteSaleResult { Success = ok };
    }
}
