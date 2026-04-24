using Moq;
using Microsoft.AspNetCore.Mvc;
using MealDraft.API.Controllers;
using MealDraft.API.Models;
using MealDraft.API.Services;

namespace MealDraft.Tests.Controllers;

public class MealsControllerTests
{
    private readonly Mock<IMealService> _mockService;
    private readonly MealsController _controller;

    public MealsControllerTests()
    {
        _mockService = new Mock<IMealService>();
        _controller = new MealsController(_mockService.Object);
    }

    [Fact]
    public void GetAll_ReturnsOkWithMeals()
    {
        // Arrange
        _mockService.Setup(s => s.GetAll()).Returns(new List<Meal>
        {
            new Meal { Name = "Spaghetti", Description = "Italian", PrepTimeMinutes = 30 }
        });

        // Act
        var result = _controller.GetAll();

        // Assert
        var ok = Assert.IsType<OkObjectResult>(result);
        var meals = Assert.IsType<List<Meal>>(ok.Value);
        Assert.Single(meals);
    }

    [Fact]
    public void GetById_ReturnsNotFound_WhenMealMissing()
    {
        // Arrange
        _mockService.Setup(s => s.GetById(It.IsAny<Guid>())).Returns((Meal?)null);

        // Act
        var result = _controller.GetById(Guid.NewGuid());

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Delete_ReturnsNoContent_WhenSuccessful()
    {
        // Arrange
        _mockService.Setup(s => s.Delete(It.IsAny<Guid>())).Returns(true);

        // Act
        var result = _controller.Delete(Guid.NewGuid());

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}