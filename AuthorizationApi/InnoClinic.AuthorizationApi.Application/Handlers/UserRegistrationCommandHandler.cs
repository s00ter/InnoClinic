using Hellang.Middleware.ProblemDetails;
using InnoClinic.Application.Commands;
using InnoClinic.Application.IService;
using InnoClinic.Application.Models.Email;
using InnoClinic.BusinessLogic.Entities;
using InnoClinic.Shared.Constants;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.Application.Handlers;

public class UserRegistrationCommandHandler(
    UserManager<User> userManager,
    IEmailService emailService
    ) 
    : IRequestHandler<UserRegistrationCommand, bool>
{
    public async Task<bool> Handle(UserRegistrationCommand request, CancellationToken cancellationToken)
    {
        var appUser = new User
        {
            FirstName = request.Request.FirstName,
            LastName = request.Request.LastName,
            UserName = request.Request.Email,
            Email = request.Request.Email,
        };
        
        var user = await userManager.FindByEmailAsync(appUser.Email);
        if (user != null)
        {
            throw new ProblemDetailsException(new ProblemDetails() { Title = "This Email is already in use." });
        }
        
        var createResult = await userManager.CreateAsync(appUser, request.Request.Password);
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
            var problemDetails = new ProblemDetails { Title = "Add role to account failed" };
            problemDetails.Extensions.Add("errors", errors);
            throw new ProblemDetailsException(problemDetails);
        }
        
        var token = await userManager.GenerateEmailConfirmationTokenAsync(appUser);
        
        var message = new Message([appUser.Email], "Email confirmation token", token, null);
        
        await emailService.SendEmail(message);

        return true;
    }
}