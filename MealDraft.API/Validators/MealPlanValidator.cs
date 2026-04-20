using FluentValidation;
using MealDraft.API.Models;

namespace MealDraft.API.Validators;

public class MealPlanValidator : AbstractValidator<MealPlan>
{
    public MealPlanValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Meal plan must have a name.");

        RuleFor(x => x.Entries)
            .NotEmpty().WithMessage("Meal plan must have at least one entry.");
    }
}