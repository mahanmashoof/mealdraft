using FluentValidation;
using MealDraft.API.Models;

namespace MealDraft.API.Validators;

public class MealValidator : AbstractValidator<Meal>
{
    public MealValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Meal must have a name.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Meal must have a description.");

        RuleFor(x => x.PrepTimeMinutes)
            .GreaterThan(0).WithMessage("Prep time must be greater than 0.");

        RuleFor(x => x.Ingredients)
            .NotEmpty().WithMessage("Meal must have at least one ingredient.");
    }
}