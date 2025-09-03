using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

public class DeleteSaleCommand : IRequest<DeleteSaleResult>
{
    public Guid Id { get; set; }
    public DeleteSaleCommand(Guid id) => Id = id;
    public ValidationResultDetail Validate()
    {
        var validator = new DeleteSaleCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}
