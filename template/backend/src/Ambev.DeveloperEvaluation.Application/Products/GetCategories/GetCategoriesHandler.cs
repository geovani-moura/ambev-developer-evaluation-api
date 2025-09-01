using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetCategories;

public class GetCategoriesHandler : IRequestHandler<GetCategoriesCommand, GetCategoriesResult>
{
    private readonly IProductRepository _repo;
    public GetCategoriesHandler(IProductRepository repo) => _repo = repo;

    public async Task<GetCategoriesResult> Handle(GetCategoriesCommand request, CancellationToken ct)
    {
        return new GetCategoriesResult { Categories = (await _repo.GetCategoriesAsync(ct)).ToList() };
    }
}
