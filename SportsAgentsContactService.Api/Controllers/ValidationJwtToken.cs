using Microsoft.AspNetCore.Mvc;
using SportsAgentsContactService.Api.Dto;
using SportsAgentsContactService.Application.Interfaces;

namespace SportsAgentsContactService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ValidationJwtToken : Controller
{
    // Vars
    private readonly IValidationJwtTokenService _validationJwtTokenService;

    // Constructor
    public ValidationJwtToken(IValidationJwtTokenService validationJwtTokenService)
    {
        _validationJwtTokenService = validationJwtTokenService;
    }

    // Validate the JWT token
    [HttpPost]
    public IActionResult ValidateJwtToken([FromBody] JwtTokenDto jwtTokenDto)
    {
        var result = _validationJwtTokenService.ValidateJwtToken(jwtTokenDto.Token);
        return Ok(result);
    }
}