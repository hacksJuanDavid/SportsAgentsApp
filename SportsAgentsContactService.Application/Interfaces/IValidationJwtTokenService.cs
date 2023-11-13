namespace SportsAgentsContactService.Application.Interfaces;

public interface IValidationJwtTokenService
{
    // Validate the JWT token
    public bool ValidateJwtToken(string token);
}