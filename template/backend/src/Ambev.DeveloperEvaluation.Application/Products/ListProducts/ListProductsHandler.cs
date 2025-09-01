using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts;

public class ListProductsHandler : IRequestHandler<ListProductsCommand, ListProductsResult>
{
    private readonly IProductRepository _repo;
    private readonly IMapper _mapper;
    public ListProductsHandler(IProductRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<ListProductsResult> Handle(ListProductsCommand r, CancellationToken ct)
    {
        var page = await _repo.ListAsync(r.Page, r.Size, r.Order, ct);
        var result = _mapper.Map<ListProductsResult>(page);
        return result;
    }
}
