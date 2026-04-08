using MealDraft.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace MealDraft.API.Controllers;

[ApiController]
[Route("[controller]")]
public class MealsController : ControllerBase
{
    private static readonly List<Meal> _meals = [];

    [HttpGet]
    public IActionResult GetAll() => Ok(_meals);

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var meal = _meals.FirstOrDefault(m => m.Id == id);
        return meal is null ? NotFound() : Ok(meal);
    }

    [HttpPost]
    public IActionResult Create(Meal meal)
    {
        _meals.Add(meal);
        return CreatedAtAction(nameof(GetAll), new { id = meal.Id }, meal);
    }

    [HttpPut("{id}")]
    public IActionResult Update(Guid id, Meal updated)
    {
        var meal = _meals.FirstOrDefault(m => m.Id == id);
        if (meal is null) return NotFound();

        meal.Name = updated.Name;
        meal.Description = updated.Description;
        meal.PrepTimeMinutes = updated.PrepTimeMinutes;
        meal.Ingredients = updated.Ingredients;

        return Ok(meal);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var meal = _meals.FirstOrDefault(m => m.Id == id);
        if (meal is null) return NotFound();

        _meals.Remove(meal);
        return NoContent();
    }
}