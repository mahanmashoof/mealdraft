using MealDraft.API.Models;

namespace MealDraft.API.Repositories;

public interface IMealPlanRepository
{
    MealPlan Add(MealPlan plan);
    MealPlan? GetById(Guid id);
    List<MealPlan> GetAll();
}