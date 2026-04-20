using Microsoft.EntityFrameworkCore;
using MealDraft.API.Models;

namespace MealDraft.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Meal> Meals { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<MealPlan> MealPlans { get; set; }
    public DbSet<MealPlanEntry> MealPlanEntries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Meal>()
            .HasMany(m => m.Ingredients)
            .WithOne()
            .HasForeignKey(i => i.MealId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}