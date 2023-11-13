using Microsoft.AspNetCore.Mvc;
using SportsAgentsUserService.Api.Dto;
using SportsAgentsUserService.Application.Interfaces;

namespace SportsAgentsUserService.Api.Controllers;

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
        var token = await _authService.LoginAsync(authDto.Email, authDto.Password);
        return Ok(token);
    }

    // POST: /api/auth/register
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var token = await _authService.RegisterAsync(registerDto.Username, registerDto.Email, registerDto.Password);
        return Ok(token);
    }
}