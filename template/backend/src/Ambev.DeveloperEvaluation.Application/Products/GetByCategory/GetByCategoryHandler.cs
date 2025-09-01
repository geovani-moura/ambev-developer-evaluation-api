using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetByCategory;

public class GetByCategoryHandler : IRequestHandler<GetByCategoryCommand, GetByCategoryResult>
{
    private readonly IProductRepository _repo;
    private readonly IMapper _mapper;
    public GetByCategoryHandler(IProductRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<GetByCategoryResult> Handle(GetByCategoryCommand r, CancellationToken ct)
    {
        var list = await _repo.ListByCategoryAsync(r.Category, r.Page, r.Size, r.Order, ct);
        var result = _mapper.Map<GetByCategoryResult>(list);
        return result;
    }
}
