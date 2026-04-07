namespace MealDraft.API.Models;

public class MealPlan
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty; // e.g. week x
    public List<MealPlanEntry> Entries { get; set; } = new();
}

public class MealPlanEntry
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DayOfWeek Day { get; set; }
    public Guid MealId { get; set; }
}