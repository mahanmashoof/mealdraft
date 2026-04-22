using MealDraft.API.Models;
using MealDraft.API.Repositories;

namespace MealDraft.API.Services;

public class MealPlanService : IMealPlanService
{
    private readonly IMealPlanRepository _mealPlanRepository;
    private readonly IMealRepository _mealRepository;

    public MealPlanService(
        IMealPlanRepository mealPlanRepository,
        IMealRepository mealRepository)
    {
        _mealPlanRepository = mealPlanRepository;
        _mealRepository = mealRepository;
    }

    public MealPlan Create(MealPlan plan) => _mealPlanRepository.Add(plan);

    public MealPlan? GetById(Guid id) => _mealPlanRepository.GetById(id);

    public List<MealPlan> GetAll() => _mealPlanRepository.GetAll();

    public List<ShoppingListItem> GetShoppingList(Guid mealPlanId)
    {
        var plan = _mealPlanRepository.GetById(mealPlanId);
        if (plan is null) return new List<ShoppingListItem>();

        var shoppingList = new List<ShoppingListItem>();

        foreach (var entry in plan.Entries)
        {
            var meal = _mealRepository.GetById(entry.MealId);
            if (meal is null) continue;

            foreach (var ingredient in meal.Ingredients)
            {
                // find existing item with same name and unit
                var existing = shoppingList.FirstOrDefault(s =>
                    s.IngredientName == ingredient.Name &&
                    s.Unit == ingredient.Unit);

                if (existing is not null)
                    existing.TotalAmount += ingredient.Amount; // combine duplicates
                else
                    shoppingList.Add(new ShoppingListItem
                    {
                        IngredientName = ingredient.Name,
                        TotalAmount = ingredient.Amount,
                        Unit = ingredient.Unit
                    });
            }
        }

        return shoppingList;
    }
}