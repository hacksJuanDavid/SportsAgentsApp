using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SportsAgentsContactService.Application.Interfaces;

namespace SportsAgentsContactService.Application.Services;

public class ValidationJwtTokenService : IValidationJwtTokenService
{
    // Vars
    private readonly string _secretKey;

    // Constructor
    public ValidationJwtTokenService(IConfiguration config)
    {
        _secretKey = config.GetSection("AppSettings").GetSection("SecretKey").Value!;
    }

    // Validate the JWT token
    public bool ValidateJwtToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secretKey);
        tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        }, out _);

        return true;
    }
}