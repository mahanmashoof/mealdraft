namespace MealDraft.API.Models;

public class ShoppingListItem
{
    public string IngredientName { get; set; } = string.Empty;
    public double TotalAmount { get; set; }
    public string Unit { get; set; } = string.Empty;
}