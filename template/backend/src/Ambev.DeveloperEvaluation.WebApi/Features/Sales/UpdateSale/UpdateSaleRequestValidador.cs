using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
{
    public UpdateSaleRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Sale ID is required.");

        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Sale date is required.");

        // Cliente
        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("Customer ID is required.");

        RuleFor(x => x.CustomerName)
            .NotEmpty().WithMessage("Customer name is required.");

        RuleFor(x => x.CustomerEmail)
            .NotEmpty().WithMessage("Customer email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.CustomerPhone)
            .NotEmpty().WithMessage("Customer phone is required.");

        // Filial
        RuleFor(x => x.Branch)
            .NotEmpty().WithMessage("Branch is required.");

        // Itens
        RuleFor(x => x.Items)
            .NotEmpty().WithMessage("At least one item must be provided.");

        RuleForEach(x => x.Items).SetValidator(new UpdateSaleItemRequestValidator());
    }
}

public class UpdateSaleItemRequestValidator : AbstractValidator<UpdateSaleItemRequest>
{
    public UpdateSaleItemRequestValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("Product ID is required.");

        RuleFor(x => x.ProductTitle)
            .NotEmpty().WithMessage("Product title is required.");

        RuleFor(x => x.ProductCategory)
            .NotEmpty().WithMessage("Product category is required.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero.")
            .LessThanOrEqualTo(20).WithMessage("Cannot sell more than 20 items of the same product.");

        RuleFor(x => x.UnitPrice)
            .GreaterThan(0).WithMessage("Unit price must be greater than zero.");
    }
}
