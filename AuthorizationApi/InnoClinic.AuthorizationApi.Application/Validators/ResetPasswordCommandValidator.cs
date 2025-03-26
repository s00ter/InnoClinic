using FluentValidation;
using InnoClinic.Application.Commands;

namespace InnoClinic.Application.Validators;

public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        RuleFor(x => x.Request.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(5).WithMessage("Password must be at least 5 characters")
            .OverridePropertyName("Password");
        
        RuleFor(x => x.Request.ConfirmPassword)
            .Equal(x => x.Request.Password).WithMessage("Passwords do not match")
            .OverridePropertyName("ConfirmPassword");

        RuleFor(x => x.Request.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid Email format")
            .OverridePropertyName("Email");

        RuleFor(x => x.Request.Token)
            .NotEmpty().WithMessage("Token is required")
            .OverridePropertyName("Token");
    }
}