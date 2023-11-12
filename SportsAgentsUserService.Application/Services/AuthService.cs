using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SportsAgentsUserService.Application.Interfaces;
using SportsAgentsUserService.Domain.Entities;
using SportsAgentsUserService.Domain.Exceptions;
using SportsAgentsUserService.Domain.Interfaces.Repositories;

namespace SportsAgentsUserService.Application.Services;

public class AuthService : IAuthService
{
    // Vars
    private readonly IUserRepository _userRepository;
    private readonly string _secretKey;

    // Constructor
    public AuthService(IUserRepository userRepository, IConfiguration config)
    {
        _userRepository = userRepository;
        _secretKey = config.GetSection("AppSettings").GetSection("SecretKey").Value;
    }

    public async Task<string> LoginAsync(string email, string password)
    {
        // Get User by Email
        var user = await _userRepository.GetByEmailAsync(email);

        // Validation User Exists
        if (user == null)
        {
            throw new NotFoundException($"User with Email={email} Not Found");
        }

        // Validation Password is Correct
        if (user.Password != password)
        {
            throw new BadRequestException("Password is Incorrect");
        }

        // Create Token
        var keyBytes = Encoding.ASCII.GetBytes(_secretKey!);
        var claims = new ClaimsIdentity();

        // Add Claims
        if (user.Email != null)
        {
            claims.AddClaim(new Claim(ClaimTypes.Email, user.Email));
        }

        // Token descriptor
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claims,
            Expires = DateTime.UtcNow.AddMinutes(5),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes),
                SecurityAlgorithms.HmacSha256Signature)
        };

        // Token Handler
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        // String Token
        var tokenString = tokenHandler.WriteToken(token);

        // Return Token
        return tokenString;
    }

    public async Task<object?> RegisterAsync(string username, string email, string password)
    {
        try
        {
            // Validation User Exists
            var userExists = await _userRepository.GetByEmailAsync(email);

            // If user exists, throw BadRequestException
            if (userExists != null)
            {
                throw new BadRequestException($"User with Email={email} Already Exists");
            }

            // Create User
            var user = new User
            {
                Username = username,
                Email = email,
                Password = password,
                Role = "User"
            };

            // Add User
            var userCreated = await _userRepository.AddAsync(user);
            return userCreated;
        }
        catch (NotFoundException)
        {
            // If NotFoundException is thrown by GetByEmailAsync, it means the user does not exist, so proceed with registration
            var user = new User
            {
                Username = username,
                Email = email,
                Password = password,
                Role = "User"
            };

            // Add User
            var userCreated = await _userRepository.AddAsync(user);
            return userCreated;
        }
        catch (Exception ex)
        {
            // Handle other exceptions and return a proper error response
            throw new InternalServerErrorException("Error occurred while registering the user.", ex);
        }
    }
}