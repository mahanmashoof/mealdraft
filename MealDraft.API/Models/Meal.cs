namespace MealDraft.API.Models;

public class Meal
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int PrepTimeMinutes { get; set; }
    public List<Ingredient> Ingredients { get; set; } = new();
}