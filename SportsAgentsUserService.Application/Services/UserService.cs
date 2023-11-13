using SportsAgentsUserService.Application.Interfaces;
using SportsAgentsUserService.Domain.Entities;
using SportsAgentsUserService.Domain.Exceptions;
using SportsAgentsUserService.Domain.Interfaces.Repositories;

namespace SportsAgentsUserService.Application.Services;

public class UserService : IUserService
{
    // Vars 
    private readonly IUserRepository _userRepository;

    // Constructor
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    // Methods
    // Add User
    public async Task<User> AddUserAsync(User user)
    {
        // Validation User 
        if (user == null)
        {
            throw new BadRequestException("User is null");
        }

        // Validation User Exists with Id
        var userExists = await _userRepository.GetByIdAsync(user.Id);
        if (userExists != null)
        {
            throw new BadRequestException($"User with Id={user.Id} Already Exists");
        }

        // Add User
        var userCreated = await _userRepository.AddAsync(user);
        return userCreated;
    }

    // Get User By Id
    public async Task<User?> GetUserByIdAsync(int id)
    {
        // Validation User Exists
        var userExists = await _userRepository.GetByIdAsync(id);
        if (userExists == null)
        {
            throw new NotFoundException($"User with Id={id} Not Found");
        }

        // Get User
        var user = await _userRepository.GetByIdAsync(id);
        return user;
    }

    // Get User By Email
    public async Task<User> GetUserByEmailAsync(string email)
    {
        // Validation User Exists
        var userExists = await _userRepository.GetByEmailAsync(email);
        if (userExists == null)
        {
            throw new NotFoundException($"User with Email={email} Not Found");
        }

        // Get User
        var user = await _userRepository.GetByEmailAsync(email);
        return user;
    }

    // Get All Users
    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        // Get All Users
        var users = await _userRepository.GetAllAsync();
        return users;
    }

    // Update User
    public async Task<User?> UpdateUserAsync(User user)
    {
        // Validation User 
        if (user == null)
        {
            throw new BadRequestException("User is null");
        }

        // Validation User Exists
        var userExists = await _userRepository.GetByIdAsync(user.Id);
        if (userExists == null)
        {
            throw new NotFoundException($"User with Id={user.Id} Not Found");
        }

        // Update User
        var userUpdated = await _userRepository.UpdateAsync(user);
        return userUpdated;
    }

    // Delete User
    public async Task<User?> DeleteUserAsync(int id)
    {
        // Validation User Exists
        var userExists = await _userRepository.GetByIdAsync(id);
        if (userExists == null)
        {
            throw new NotFoundException($"User with Id={id} Not Found");
        }

        // Delete User
        var userDeleted = await _userRepository.DeleteAsync(userExists);
        return userDeleted;
    }
}