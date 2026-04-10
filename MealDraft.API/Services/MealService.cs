using MealDraft.API.Models;

namespace MealDraft.API.Services;

public class MealService : IMealService
{
    private readonly List<Meal> _meals = new();

    public List<Meal> GetAll() => _meals;

    public Meal? GetById(Guid id) => _meals.FirstOrDefault(m => m.Id == id);

    public Meal Create(Meal meal)
    {
        _meals.Add(meal);
        return meal;
    }

    public Meal? Update(Guid id, Meal updated)
    {
        var meal = _meals.FirstOrDefault(m => m.Id == id);
        if (meal is null) return null;

        meal.Name = updated.Name;
        meal.Description = updated.Description;
        meal.PrepTimeMinutes = updated.PrepTimeMinutes;
        meal.Ingredients = updated.Ingredients;

        return meal;
    }

    public bool Delete(Guid id)
    {
        var meal = _meals.FirstOrDefault(m => m.Id == id);
        if (meal is null) return false;

        _meals.Remove(meal);
        return true;
    }
}