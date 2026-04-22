using Microsoft.AspNetCore.Mvc;
using MealDraft.API.Models;
using MealDraft.API.Services;

namespace MealDraft.API.Controllers;

[ApiController]
[Route("[controller]")]
public class MealPlansController : ControllerBase
{
    private readonly IMealPlanService _mealPlanService;

    public MealPlansController(IMealPlanService mealPlanService)
    {
        _mealPlanService = mealPlanService;
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(_mealPlanService.GetAll());

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var plan = _mealPlanService.GetById(id);
        return plan is null ? NotFound() : Ok(plan);
    }

    [HttpPost]
    public IActionResult Create(MealPlan plan)
    {
        var created = _mealPlanService.Create(plan);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpGet("{id}/shopping-list")]
    public IActionResult GetShoppingList(Guid id)
    {
        var list = _mealPlanService.GetShoppingList(id);
        return Ok(list);
    }
}