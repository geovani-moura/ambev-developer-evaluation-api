using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    private readonly ISaleRepository _repository;
    private readonly IMapper _mapper;

    public UpdateSaleHandler(ISaleRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        // Verifica se a venda existe
        var existingSale = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (existingSale == null)
            throw new KeyNotFoundException($"Sale with ID {request.Id} not found.");

        var sale = _mapper.Map<Sale>(request);
        foreach (var item in request.Items)
        {
            sale.AddItem(
                item.ProductId,
                item.ProductTitle,
                item.ProductCategory,
                item.Quantity,
                item.UnitPrice,
                item.Id
            );
        }

        var updatedSale = await _repository.UpdateAsync(sale, cancellationToken);
        return _mapper.Map<UpdateSaleResult>(updatedSale);
    }

}
