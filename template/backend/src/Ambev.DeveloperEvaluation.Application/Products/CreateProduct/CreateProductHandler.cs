using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
{
    private readonly IProductRepository _repo;
    private readonly IMapper _mapper;

    public CreateProductHandler(IProductRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<CreateProductResult> Handle(CreateProductCommand r, CancellationToken ct)
    {
        var entity = _mapper.Map<Product>(r);
        var create = await _repo.CreateAsync(entity, ct);
        var result = _mapper.Map<CreateProductResult>(create);
        return result;
    }
}
