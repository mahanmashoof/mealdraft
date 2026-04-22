using MealDraft.API.Models;

namespace MealDraft.API.Services;

public interface IMealPlanService
{
    MealPlan Create(MealPlan plan);
    MealPlan? GetById(Guid id);
    List<MealPlan> GetAll();
    List<ShoppingListItem> GetShoppingList(Guid mealPlanId);
}