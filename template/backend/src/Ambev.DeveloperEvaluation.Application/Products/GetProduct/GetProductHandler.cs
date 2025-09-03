using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

public class GetProductHandler : IRequestHandler<GetProductCommand, GetProductResult>
{
    private readonly IProductRepository _repo;
    private readonly IMapper _mapper;
    public GetProductHandler(IProductRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<GetProductResult> Handle(GetProductCommand command, CancellationToken cancellationToken)
    {

        var validator = new GetProductCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);


        var user = await _repo.GetByIdAsync(command.Id, cancellationToken);
        if (user == null)
            throw new KeyNotFoundException($"Product with ID {command.Id} not found");

        return _mapper.Map<GetProductResult>(user);
    }
}
