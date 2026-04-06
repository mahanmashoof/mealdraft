using Microsoft.AspNetCore.Mvc;

namespace MealDraft.API.Controllers;

[ApiController]
[Route("[controller]")]
public class MealsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok("MealDraft is alive 🍽️");
    }
}