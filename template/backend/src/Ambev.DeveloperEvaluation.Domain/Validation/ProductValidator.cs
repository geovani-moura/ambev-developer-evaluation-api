using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(p => p.Title)
            .NotEmpty().WithMessage("Product title is required.");

        RuleFor(p => p.Price)
            .GreaterThan(0).WithMessage("Product price must be greater than zero.");

        RuleFor(p => p.Category)
            .NotEmpty().WithMessage("Category is required.");

        // Rating simples (mantendo só o essencial)
        RuleFor(p => p.RatingRate)
            .InclusiveBetween(0, 5).WithMessage("Rate must be between 0 and 5.");

        RuleFor(p => p.RatingCount)
            .GreaterThanOrEqualTo(0).WithMessage("Count must be a non-negative value.");
    }
}
