using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

public record GetProductCommand : IRequest<GetProductResult>
{
    public Guid Id { get; set; }
    public GetProductCommand(Guid id) => Id = id;
    public ValidationResultDetail Validate()
    {
        var validator = new GetProductCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}
