using MealDraft.API.Models;

namespace MealDraft.API.Services;

public interface IMealService
{
    List<Meal> GetAll();
    Meal? GetById(Guid id);
    Meal Create(Meal meal);
    Meal? Update(Guid id, Meal updated);
    bool Delete(Guid id);
}