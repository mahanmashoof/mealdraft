using Microsoft.AspNetCore.Mvc;
using MealDraft.API.Models;

namespace MealDraft.API.Controllers;

[ApiController]
[Route("[controller]")]
public class MealPlansController : ControllerBase
{
    private static readonly List<MealPlan> _plans = new();

    [HttpGet]
    public IActionResult GetAll() => Ok(_plans);

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var plan = _plans.FirstOrDefault(p => p.Id == id);
        return plan is null ? NotFound() : Ok(plan);
    }

    [HttpPost]
    public IActionResult Create(MealPlan plan)
    {
        _plans.Add(plan);
        return CreatedAtAction(nameof(GetById), new { id = plan.Id }, plan);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var plan = _plans.FirstOrDefault(p => p.Id == id);
        if (plan is null) return NotFound();

        _plans.Remove(plan);
        return NoContent();
    }
}