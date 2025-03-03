using FluentValidation;
using InnoClinic.Application.Dto.Account;
using InnoClinic.BusinessLogic.Entities;
using Microsoft.AspNetCore.Identity;

namespace InnoClinic.Authorization.Validators;

public class UserAuthenticationValidators : AbstractValidator<UserAuthenticationRequest>
{
    public UserAuthenticationValidators(UserManager<User> userManager)
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid Email format");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(5).WithMessage("Password must be at least 5 characters");
    }
}