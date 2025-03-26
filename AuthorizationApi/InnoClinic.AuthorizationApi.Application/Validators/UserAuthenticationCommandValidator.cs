using FluentValidation;
using InnoClinic.Application.Commands;

namespace InnoClinic.Application.Validators;

public class UserAuthenticationCommandValidator : AbstractValidator<UserAuthenticationCommand>
{
    public UserAuthenticationCommandValidator()
    {
        RuleFor(x => x.Request.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid Email format")
            .OverridePropertyName("Email");

        RuleFor(x => x.Request.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(5).WithMessage("Password must be at least 5 characters")
            .OverridePropertyName("Password");
    }
}