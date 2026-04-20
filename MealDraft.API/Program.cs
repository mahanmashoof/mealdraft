using MealDraft.API.Services;
using MealDraft.API.Data;
using Microsoft.EntityFrameworkCore;
using MealDraft.API.Repositories;
using FluentValidation.AspNetCore;
using MealDraft.API.Validators;
using FluentValidation;
using MealDraft.API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMealService, MealService>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=mealdraft.db"));
builder.Services.AddScoped<IMealRepository, MealRepository>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<Meal>, MealValidator>();
builder.Services.AddScoped<IValidator<Ingredient>, IngredientValidator>();
builder.Services.AddScoped<IValidator<MealPlan>, MealPlanValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();

app.MapControllers();

app.Run();
