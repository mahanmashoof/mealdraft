using MealDraft.API.Models;

namespace MealDraft.API.Services;

public interface IAuthService
{
    Task<string?> RegisterAsync(RegisterRequest request);
    Task<string?> LoginAsync(LoginRequest request);
}