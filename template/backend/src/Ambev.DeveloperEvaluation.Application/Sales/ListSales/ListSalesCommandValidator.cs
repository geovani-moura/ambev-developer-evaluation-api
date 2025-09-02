using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales;

public class ListSalesCommandValidator : AbstractValidator<ListSalesCommand>
{
    public ListSalesCommandValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThan(0).WithMessage("Page must be greater than zero.");

        RuleFor(x => x.Size)
            .GreaterThan(0).WithMessage("Size must be greater than zero.")
            .LessThanOrEqualTo(100).WithMessage("Size cannot be greater than 100.");
    }
}
