using FluentValidation;
using InnoClinic.Application.Commands;

namespace InnoClinic.Application.Validators;

public class EmailVerificationCommandValidator : AbstractValidator<EmailVerificationCommand>
{
    public EmailVerificationCommandValidator()
    {
        RuleFor(x => x.Request.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid Email format");

        RuleFor(x => x.Request.Token)
            .NotEmpty().WithMessage("Token is required");
    }
}