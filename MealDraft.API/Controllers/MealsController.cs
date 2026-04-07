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

    [HttpPost]
    public IActionResult Create(Meal meal)
    {
        _meals.Add(meal);
        return CreatedAtAction(nameof(GetAll), new { id = meal.Id }, meal);
    }
}