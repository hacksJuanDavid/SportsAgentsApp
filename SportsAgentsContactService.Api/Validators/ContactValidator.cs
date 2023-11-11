using FluentValidation;
using SportsAgentsContactService.Api.Dto;

namespace SportsAgentsContactService.Api.Validators;

public class ContactValidator : AbstractValidator<ContactDto>
{
    public ContactValidator()
    {
        RuleFor(x => x.Username).NotEmpty().WithMessage("Username name is required");
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
        RuleFor(x => x.Message).NotEmpty().WithMessage("Message is required");
    }
}