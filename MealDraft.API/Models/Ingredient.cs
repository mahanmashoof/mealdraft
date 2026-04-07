namespace MealDraft.API.Models;

public class Ingredient
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Unit { get; set; } = string.Empty; // e.g. "grams", "cups", "pieces"
    public double Amount { get; set; }
}