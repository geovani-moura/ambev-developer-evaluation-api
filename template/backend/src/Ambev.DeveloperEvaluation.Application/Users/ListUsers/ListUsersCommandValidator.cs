using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers;

/// <summary>
/// Validator for <see cref="ListUsersCommand"/>.
/// </summary>
public class ListUsersCommandValidator : AbstractValidator<ListUsersCommand>
{
    public ListUsersCommandValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThan(0).WithMessage("Page number must be greater than 0.");

        RuleFor(x => x.Size)
            .InclusiveBetween(1, 100).WithMessage("Size must be between 1 and 100.");

        RuleFor(x => x.Order)
            .Must(BeAValidOrderField)
            .When(x => !string.IsNullOrWhiteSpace(x.Order))
            .WithMessage("Invalid order field. Allowed fields: id, username, email, status, role.");
    }

    private static bool BeAValidOrderField(string? order)
    {
        if (string.IsNullOrWhiteSpace(order))
            return true;

        var allowedFields = new[] { "id", "username", "email", "status", "role" };

        var fields = order
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(x => x.Split(' ')[0].ToLowerInvariant());

        return fields.All(f => allowedFields.Contains(f));
    }
}
