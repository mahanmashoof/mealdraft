using MealDraft.API.Data;
using MealDraft.API.Models;

namespace MealDraft.API.Repositories;

public class MealRepository : IMealRepository
{
    private readonly AppDbContext _db;

    public MealRepository(AppDbContext db)
    {
        _db = db;
    }

    public List<Meal> GetAll() => _db.Meals.ToList();

    public Meal? GetById(Guid id) => _db.Meals.FirstOrDefault(m => m.Id == id);

    public Meal Add(Meal meal)
    {
        _db.Meals.Add(meal);
        _db.SaveChanges();
        return meal;
    }

    public Meal? Update(Meal meal)
    {
        _db.Meals.Update(meal);
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