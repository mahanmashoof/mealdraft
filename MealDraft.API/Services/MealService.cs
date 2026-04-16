using MealDraft.API.Models;
using MealDraft.API.Repositories;

namespace MealDraft.API.Services;

public class MealService : IMealService
{
    private readonly IMealRepository _repository;

    public MealService(IMealRepository repository) => _repository = repository;

    public List<Meal> GetAll() => _repository.GetAll();

    public Meal? GetById(Guid id) => _repository.GetById(id);

    public Meal Create(Meal meal) =>
        _repository.Add(meal);

    public Meal? Update(Guid id, Meal updated)
    {
        var meal = _repository.GetById(id);
        if (meal is null) return null;

        meal.Name = updated.Name;
        meal.Description = updated.Description;
        meal.PrepTimeMinutes = updated.PrepTimeMinutes;
        //meal.Ingredients = updated.Ingredients; // this will be done separately

        return _repository.Update(meal);
    }

    public bool Delete(Guid id) => _repository.Delete(id);
}