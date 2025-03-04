using FluentValidation;
using InnoClinic.Application.Dto.Account;
using InnoClinic.BusinessLogic.Entities;
using Microsoft.AspNetCore.Identity;

namespace InnoClinic.Application.Validators;

public class EmailVerificationValidator : AbstractValidator<EmailVerificationRequest>
{
    public EmailVerificationValidator(UserManager<User> userManager)
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid Email format");

        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("Token is required");
    }
}