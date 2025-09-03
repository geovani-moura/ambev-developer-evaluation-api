using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

internal class GetSaleCommandValidator : AbstractValidator<GetSaleCommand>
{
    public GetSaleCommandValidator()
    {
        RuleFor(x => x.Id)
             .NotEmpty()
             .WithMessage("User ID is required");
    }
}
