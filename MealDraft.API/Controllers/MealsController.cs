using MealDraft.API.Models;
using MealDraft.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace MealDraft.API.Controllers;

[ApiController]
[Route("[controller]")]
public class MealsController : ControllerBase
{
    private readonly IMealService _mealService;

    public MealsController(IMealService mealService)
    {
        _mealService = mealService;
    }

    [Authorize]
    [HttpGet]
    public IActionResult GetAll() => Ok(_mealService.GetAll());

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var meal = _mealService.GetById(id);
        return meal is null ? NotFound() : Ok(meal);
    }

    [HttpPost]
    public IActionResult Create(Meal meal)
    {
        var created = _mealService.Create(meal);
        return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public IActionResult Update(Guid id, Meal updated)
    {
        var meal = _mealService.Update(id, updated);
        return meal is null ? NotFound() : Ok(meal);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var deleted = _mealService.Delete(id);
        return deleted ? NoContent() : NotFound();
    }
}