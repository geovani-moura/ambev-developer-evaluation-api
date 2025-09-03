using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

public class GetSaleHandler : IRequestHandler<GetSaleCommand, GetSaleResult>
{
    private readonly ISaleRepository _repo;
    private readonly IMapper _mapper;
    public GetSaleHandler(ISaleRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<GetSaleResult> Handle(GetSaleCommand command, CancellationToken cancellationToken)
    {

        var validator = new GetSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);


        var user = await _repo.GetByIdAsync(command.Id, cancellationToken);
        if (user == null)
            throw new KeyNotFoundException($"Sale with ID {command.Id} not found");

        return _mapper.Map<GetSaleResult>(user);
    }
}
