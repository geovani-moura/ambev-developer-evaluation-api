using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSales;

/// <summary>
/// Validator for <see cref="ListSalesRequest"/>.
/// </summary>
public class ListSalesRequestValidator : AbstractValidator<ListSalesRequest>
{
    public ListSalesRequestValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThan(0).WithMessage("Page number must be greater than 0.");

        RuleFor(x => x.Size)
            .InclusiveBetween(1, 100).WithMessage("Size must be between 1 and 100.");

        RuleFor(x => x.Order)
            .Must(BeAValidOrderField)
            .When(x => !string.IsNullOrWhiteSpace(x.Order))
            .WithMessage("Invalid order field. Allowed fields: id, salenumber, date, customerid, customername, branch, totalamount.");
    }

    private bool BeAValidOrderField(string? order)
    {
        if (string.IsNullOrWhiteSpace(order))
            return true;

        var allowedFields = new[] { "id", "salenumber", "date", "customerid", "customername", "branch", "totalamount" };

        var fields = order
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(x => x.Split(' ')[0].ToLowerInvariant());

        return fields.All(f => allowedFields.Contains(f));
    }
}

