using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    public CreateSaleRequestValidator()
    {
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

        RuleForEach(x => x.Items).SetValidator(new CreateSaleItemRequestValidator());
    }
}
