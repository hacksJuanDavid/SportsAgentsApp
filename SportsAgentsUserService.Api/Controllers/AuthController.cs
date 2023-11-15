using Microsoft.AspNetCore.Mvc;
using SportsAgentsUserService.Api.Dto;
using SportsAgentsUserService.Application.Interfaces;

namespace SportsAgentsUserService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    // Vars
    private readonly IAuthService _authService;

    // Constructor
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    // POST: /api/auth/login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthDto authDto)
    {
        try
        {
            var token = await _authService.LoginAsync(authDto.Email, authDto.Password);
            return Ok(new { Token = token });
        }
        catch (Exception ex)
        {
            // Handle authentication failure
            return Unauthorized(new { ex.Message });
        }

    }

    // POST: /api/auth/register
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var token = await _authService.RegisterAsync(registerDto.Username, registerDto.Email, registerDto.Password);
        return Ok(token);
    }
}