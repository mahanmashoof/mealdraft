using MealDraft.API.Models;

namespace MealDraft.API.Repositories;

public interface IMealRepository
{
    List<Meal> GetAll();
    Meal? GetById(Guid id);
    Meal Add(Meal meal);
    Meal? Update(Meal meal);
    bool Delete(Guid id);
}