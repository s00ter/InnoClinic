using FluentValidation;
using Hellang.Middleware.ProblemDetails;
using InnoClinic.Application.Dto.Account;
using InnoClinic.Application.IService;
using InnoClinic.Application.Models.Email;
using InnoClinic.BusinessLogic.Entities;
using InnoClinic.Shared.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace InnoClinic.Application.Service;

public class AccountService(
    IServiceProvider serviceProvider,
    UserManager<User> userManager,
    IEmailService emailService,
    ITokenService tokenService
    ) : IAccountService
{
    public async Task<bool> Registration(UserRegistrationRequest request, CancellationToken cancellationToken)
    {
        var validator = serviceProvider.GetRequiredService<IValidator<UserRegistrationRequest>>();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            var problemDetails = new ValidationProblemDetails(validationResult.ToDictionary())
            {
                Title = "Validation Failed",
                Status = StatusCodes.Status400BadRequest
            };
            throw new ProblemDetailsException(problemDetails);
        }
        
        var appUser = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.Email,
            Email = request.Email,
        };
        
        var user = await userManager.FindByEmailAsync(appUser.Email);
        if (user != null)
        {
            throw new ProblemDetailsException(new ProblemDetails() { Title = "This Email is already in use." });
        }
        
        var createResult = await userManager.CreateAsync(appUser, request.Password!);
        if (!createResult.Succeeded)
        {
            var errors = createResult.Errors.Select(e => e.Description).ToList();
            var problemDetails = new ProblemDetails { Title = "Account creation failed" };
            problemDetails.Extensions.Add("errors", errors);
            throw new ProblemDetailsException(problemDetails);
        }
        
        var roleResult = await userManager.AddToRoleAsync(appUser, RoleConstants.User);
        if (!roleResult.Succeeded)
        {
            var errors = createResult.Errors.Select(e => e.Description).ToList();
            var problemDetails = new ProblemDetails { Title = "Account creation failed" };
            problemDetails.Extensions.Add("errors", errors);
            throw new ProblemDetailsException(problemDetails);
        }
        
        var token = await userManager.GenerateEmailConfirmationTokenAsync(appUser);
        
        var message = new Message([appUser.Email], "Email confirmation token", token, null);
        
        await emailService.SendEmail(message);

        return true;
    }

    public async Task EmailVerification(EmailVerificationRequest request, CancellationToken cancellationToken)
    {
        var validator = serviceProvider.GetRequiredService<IValidator<EmailVerificationRequest>>();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            var problemDetails = new ValidationProblemDetails(validationResult.ToDictionary())
            {
                Title = "Validation Failed",
                Status = StatusCodes.Status400BadRequest
            };
            throw new ProblemDetailsException(problemDetails);
        }
        
        var user = await userManager.FindByEmailAsync(request.Email)
            ?? throw new ProblemDetailsException(new ProblemDetails() { Title = "Account does not exist" });
        
        var isEmailVerified = await userManager.ConfirmEmailAsync(user, request.Token);
        if (!isEmailVerified.Succeeded)
        {
            throw new ProblemDetailsException(new ProblemDetails() { Title = "Invalid email verification" });
        }
    }

    public async Task<string> Authenticate(UserAuthenticationRequest request, CancellationToken cancellationToken)
    {
        var validator = serviceProvider.GetRequiredService<IValidator<UserAuthenticationRequest>>();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            var problemDetails = new ValidationProblemDetails(validationResult.ToDictionary())
            {
                Title = "Validation Failed",
                Status = StatusCodes.Status400BadRequest
            };
            throw new ProblemDetailsException(problemDetails);
        }
        
        var user = await userManager.FindByEmailAsync(request.Email)
                   ?? throw new ProblemDetailsException(new ProblemDetails() { Title = "Account does not exist" });

        var isEmailConfirmed = await userManager.IsEmailConfirmedAsync(user);
        if (!isEmailConfirmed)
        {
            throw new ProblemDetailsException(new ProblemDetails() { Title = "Email not confirmed" });
        }
        
        var isCorrect = await userManager.CheckPasswordAsync(user, request.Password);
        if (!isCorrect)
        {
            await userManager.AccessFailedAsync(user);
            throw new ProblemDetailsException(new ProblemDetails() { Title = "Invalid password" });
        }
        
        var userStatus = await userManager.IsLockedOutAsync(user);
        if (userStatus)
        {
            throw new ProblemDetailsException(new ProblemDetails() { Title = "User is locked out please try again later." });
        }
        
        await userManager.ResetAccessFailedCountAsync(user);

        var roles = await userManager.GetRolesAsync(user);
        var token = tokenService.CreateToken(user, roles);

        return token;
    }

    public async Task ForgotPassword(ForgotPasswordRequest request, CancellationToken cancellationToken)
    {
        var validator = serviceProvider.GetRequiredService<IValidator<ForgotPasswordRequest>>();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            var problemDetails = new ValidationProblemDetails(validationResult.ToDictionary())
            {
                Title = "Validation Failed",
                Status = StatusCodes.Status400BadRequest
            };
            throw new ProblemDetailsException(problemDetails);
        }
        
        var user  = await userManager.FindByEmailAsync(request.Email)
            ?? throw new ProblemDetailsException(new ProblemDetails() { Title = "Account does not exist" });
        
        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        
        var message = new Message([user.Email], "Reset password token", token, null);
        
        await emailService.SendEmail(message);
    }

    public async Task ResetPassword(ResetPasswordRequest request, CancellationToken cancellationToken)
    {
        var validator = serviceProvider.GetRequiredService<IValidator<ResetPasswordRequest>>();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            var problemDetails = new ValidationProblemDetails(validationResult.ToDictionary())
            {
                Title = "Validation Failed",
                Status = StatusCodes.Status400BadRequest
            };
            throw new ProblemDetailsException(problemDetails);
        }
        
        var user = await userManager.FindByEmailAsync(request.Email)
            ?? throw new ProblemDetailsException(new ProblemDetails() { Title = "Account does not exist" });
        
        var res = await userManager.ResetPasswordAsync(user, request.Token, request.Password);
        if (!res.Succeeded)
        {
            throw new ProblemDetailsException(new ProblemDetails() { Title = "Reset password failed" });
        }
    }
}