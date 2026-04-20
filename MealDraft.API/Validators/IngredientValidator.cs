using FluentValidation;
using MealDraft.API.Models;

namespace MealDraft.API.Validators;

public class IngredientValidator : AbstractValidator<Ingredient>
{
    public IngredientValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Ingredient must have a name.");

        RuleFor(x => x.Unit)
            .NotEmpty().WithMessage("Ingredient must have a unit.");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than 0.");
    }
}