namespace SportsAgentsUserService.Application.Interfaces;

public interface IAuthService
{
    // Login
    public Task<string> LoginAsync(string email, string password);
    
    // Register
    public Task<object?> RegisterAsync(string username, string email, string password);
}