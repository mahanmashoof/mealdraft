using MealDraft.API.Data;
using MealDraft.API.Models;
using Microsoft.EntityFrameworkCore;

namespace MealDraft.API.Repositories;

public class MealRepository : IMealRepository
{
    private readonly AppDbContext _db;

    public MealRepository(AppDbContext db)
    {
        _db = db;
    }

    public List<Meal> GetAll() => _db.Meals.Include(m => m.Ingredients).ToList();

    public Meal? GetById(Guid id) => _db.Meals.Include(m => m.Ingredients).FirstOrDefault(m => m.Id == id);

    public Meal Add(Meal meal)
    {
        _db.Meals.Add(meal);
        _db.SaveChanges();
        return meal;
    }

    public Meal? Update(Meal meal)
    {
        // detach the tracked meal so EF Core doesn't try to update it
        _db.Entry(meal).State = EntityState.Detached;

        // delete old ingredients directly
        var existingIngredients = _db.Ingredients
            .Where(i => i.MealId == meal.Id)
            .ToList();
        _db.Ingredients.RemoveRange(existingIngredients);
        _db.SaveChanges();

        // update meal fields directly
        _db.Meals.Where(m => m.Id == meal.Id)
            .ExecuteUpdate(s => s
                .SetProperty(m => m.Name, meal.Name)
                .SetProperty(m => m.Description, meal.Description)
                .SetProperty(m => m.PrepTimeMinutes, meal.PrepTimeMinutes));

        // add new ingredients
        foreach (var ingredient in meal.Ingredients)
        {
            ingredient.Id = Guid.NewGuid();
            ingredient.MealId = meal.Id;
            _db.Ingredients.Add(ingredient);
        }

        _db.SaveChanges();
        return meal;
    }

    public bool Delete(Guid id)
    {
        var meal = _db.Meals.Include(m => m.Ingredients).FirstOrDefault(m => m.Id == id);
        if (meal is null) return false;
        _db.Meals.Remove(meal);
        _db.SaveChanges();
        return true;
    }
}