using MealDraft.API.Models;
using MealDraft.API.Data;

namespace MealDraft.API.Services;

public class MealService : IMealService
{
    private readonly AppDbContext _db;

    public MealService(AppDbContext db) => _db = db;

    public List<Meal> GetAll() => _db.Meals.ToList();

    public Meal? GetById(Guid id) => _db.Meals.FirstOrDefault(m => m.Id == id);

    public Meal Create(Meal meal)
    {
        _db.Meals.Add(meal);
        _db.SaveChanges();
        return meal;
    }

    public Meal? Update(Guid id, Meal updated)
    {
        var meal = _db.Meals.FirstOrDefault(m => m.Id == id);
        if (meal is null) return null;

        meal.Name = updated.Name;
        meal.Description = updated.Description;
        meal.PrepTimeMinutes = updated.PrepTimeMinutes;
        meal.Ingredients = updated.Ingredients;

        _db.SaveChanges();
        return meal;
    }

    public bool Delete(Guid id)
    {
        var meal = _db.Meals.FirstOrDefault(m => m.Id == id);
        if (meal is null) return false;

        _db.Meals.Remove(meal);
        _db.SaveChanges();
        return true;
    }
}