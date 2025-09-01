using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.GetByCategory;

public class GetByCategoryCommandValidator : AbstractValidator<GetByCategoryCommand>
{
    public GetByCategoryCommandValidator()
    {
        RuleFor(x => x.Category).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Page).GreaterThanOrEqualTo(1);
        RuleFor(x => x.Size).InclusiveBetween(1, 100);
    }
}
