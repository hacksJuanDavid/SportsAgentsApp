using SportsAgentsUserService.Domain.Entities;

namespace SportsAgentsUserService.Application.Interfaces;

public interface IUserService
{
    //Add user
    public Task<User> AddUserAsync(User user);

    //Get user by id
    public Task<User?> GetUserByIdAsync(int id);

    //Get user by email
    public Task<User> GetUserByEmailAsync(string email);

    //Get all users
    public Task<IEnumerable<User>> GetAllUsersAsync();

    //Update user
    public Task<User?> UpdateUserAsync(User user);

    //Delete user
    public Task<User?> DeleteUserAsync(int id);
}