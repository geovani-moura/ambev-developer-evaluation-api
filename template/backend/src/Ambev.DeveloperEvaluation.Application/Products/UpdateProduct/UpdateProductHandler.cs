using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, UpdateProductResult>
{
    private readonly IProductRepository _repo;
    private readonly IMapper _mapper;

    public UpdateProductHandler(IProductRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<UpdateProductResult> Handle(UpdateProductCommand r, CancellationToken cancellationToken)
    {
        var validator = new UpdateProductCommandValidator();
        var validationResult = await validator.ValidateAsync(r, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var exists = await _repo.GetByIdAsync(r.Id, cancellationToken);
        if (exists == null)
            throw new InvalidOperationException($"Produto with id {r.Id} already exists");

        var entity = _mapper.Map<Product>(r);
        var update = await _repo.UpdateAsync(entity, cancellationToken);
        var result = _mapper.Map<UpdateProductResult>(update);
        return result;
    }
}
