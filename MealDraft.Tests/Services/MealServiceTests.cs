using Moq;
using MealDraft.API.Models;
using MealDraft.API.Repositories;
using MealDraft.API.Services;

namespace MealDraft.Tests.Services;

public class MealServiceTests
{
    private readonly Mock<IMealRepository> _mockRepo;
    private readonly MealService _service;

    public MealServiceTests()
    {
        _mockRepo = new Mock<IMealRepository>();
        _service = new MealService(_mockRepo.Object);
    }

    [Fact]
    public void GetAll_ReturnsAllMeals()
    {
        // Arrange — fake filing room returns two meals
        _mockRepo.Setup(r => r.GetAll()).Returns(new List<Meal>
        {
            new Meal { Name = "Spaghetti", Description = "Italian", PrepTimeMinutes = 30 },
            new Meal { Name = "Salad", Description = "Fresh", PrepTimeMinutes = 10 }
        });

        // Act
        var result = _service.GetAll();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(result, m => m.Name == "Spaghetti");
    }

    [Fact]
    public void GetById_ReturnsNull_WhenNotFound()
    {
        // Arrange
        _mockRepo.Setup(r => r.GetById(It.IsAny<Guid>())).Returns((Meal?)null);

        // Act
        var result = _service.GetById(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void Create_ReturnsCreatedMeal()
    {
        // Arrange
        var meal = new Meal { Name = "Pasta", Description = "Quick", PrepTimeMinutes = 20 };
        _mockRepo.Setup(r => r.Add(It.IsAny<Meal>())).Returns(meal);

        // Act
        var result = _service.Create(meal);

        // Assert
        Assert.Equal("Pasta", result.Name);
        _mockRepo.Verify(r => r.Add(It.IsAny<Meal>()), Times.Once);
    }
}