using Moq;
using MealDraft.API.Models;
using MealDraft.API.Repositories;
using MealDraft.API.Services;

namespace MealDraft.Tests.Services;

public class MealPlanServiceTests
{
    private readonly Mock<IMealPlanRepository> _mockPlanRepo;
    private readonly Mock<IMealRepository> _mockMealRepo;
    private readonly MealPlanService _service;

    public MealPlanServiceTests()
    {
        _mockPlanRepo = new Mock<IMealPlanRepository>();
        _mockMealRepo = new Mock<IMealRepository>();
        _service = new MealPlanService(_mockPlanRepo.Object, _mockMealRepo.Object);
    }

    [Fact]
    public void GetShoppingList_ReturnsCombinedIngredients()
    {
        // Arrange
        var mealId = Guid.NewGuid();
        var planId = Guid.NewGuid();

        _mockPlanRepo.Setup(r => r.GetById(planId)).Returns(new MealPlan
        {
            Id = planId,
            Name = "Week 1",
            Entries = new List<MealPlanEntry>
            {
                new MealPlanEntry { MealId = mealId, Day = DayOfWeek.Monday },
                new MealPlanEntry { MealId = mealId, Day = DayOfWeek.Wednesday }
            }
        });

        _mockMealRepo.Setup(r => r.GetById(mealId)).Returns(new Meal
        {
            Id = mealId,
            Name = "Spaghetti",
            Description = "Italian",
            PrepTimeMinutes = 30,
            Ingredients = new List<Ingredient>
            {
                new Ingredient { Name = "Spaghetti", Unit = "grams", Amount = 200 },
                new Ingredient { Name = "Ground beef", Unit = "grams", Amount = 300 }
            }
        });

        // Act
        var result = _service.GetShoppingList(planId);

        // Assert — same meal twice, so amounts should be doubled
        Assert.Equal(2, result.Count);
        var spaghetti = result.First(i => i.IngredientName == "Spaghetti");
        Assert.Equal(400, spaghetti.TotalAmount);
    }

    [Fact]
    public void GetShoppingList_ReturnsEmpty_WhenPlanNotFound()
    {
        // Arrange
        _mockPlanRepo.Setup(r => r.GetById(It.IsAny<Guid>())).Returns((MealPlan?)null);

        // Act
        var result = _service.GetShoppingList(Guid.NewGuid());

        // Assert
        Assert.Empty(result);
    }
}