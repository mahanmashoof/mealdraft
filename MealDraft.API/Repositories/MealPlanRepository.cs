using MealDraft.API.Data;
using MealDraft.API.Models;
using Microsoft.EntityFrameworkCore;

namespace MealDraft.API.Repositories;

public class MealPlanRepository : IMealPlanRepository
{
    private readonly AppDbContext _db;

    public MealPlanRepository(AppDbContext db)
    {
        _db = db;
    }

    public MealPlan Add(MealPlan plan)
    {
        _db.MealPlans.Add(plan);
        _db.SaveChanges();
        return plan;
    }

    public MealPlan? GetById(Guid id) => _db.MealPlans
        .Include(p => p.Entries)
        .FirstOrDefault(p => p.Id == id);

    public List<MealPlan> GetAll() => _db.MealPlans
        .Include(p => p.Entries)
        .ToList();
}