using FluentValidation;
using SportsAgentsUserService.Api.Dto;

namespace SportsAgentsUserService.Api.Validators;

public class UserValidator : AbstractValidator<UserDto>
{
    public UserValidator()
    {
        RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required");
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
        RuleFor(x => x.Role).NotEmpty().WithMessage("Role is required");
    }
}