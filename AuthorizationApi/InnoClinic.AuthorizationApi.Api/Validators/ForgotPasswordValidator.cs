using FluentValidation;
using InnoClinic.Application.Dto.Account;
using InnoClinic.BusinessLogic.Entities;
using Microsoft.AspNetCore.Identity;

namespace InnoClinic.Authorization.Validators;

public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordRequest>
{
    public ForgotPasswordValidator(UserManager<User> userManager)
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid Email format");
    }
}