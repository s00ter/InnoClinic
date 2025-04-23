using InnoClinic.Application.Commands;
using InnoClinic.Application.IService;
using InnoClinic.Application.Models.Email;
using InnoClinic.BusinessLogic.Entities;
using InnoClinic.Shared.Constants;
using MediatR;
using Microsoft.AspNetCore.Identity;

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
            UserName = request.Request.Email,
            Email = request.Request.Email,
        };
        
        var user = await userManager.FindByEmailAsync(appUser.Email);
        if (user != null)
        {
            throw new Exception("This Email is already in use.");
        }
        
        var createResult = await userManager.CreateAsync(appUser, request.Request.Password);
        if (!createResult.Succeeded)
        {
            var errors = string.Join("; ", createResult.Errors.Select(e => e.Description));
            throw new Exception($"Account creation failed: {errors}");
        }
        
        var roleResult = await userManager.AddToRoleAsync(appUser, RoleConstants.User);
        if (!roleResult.Succeeded)
        {
            var errors = string.Join("; ", createResult.Errors.Select(e => e.Description));
            throw new Exception($"Add role to account failed: {errors}");
        }
        
        var token = await userManager.GenerateEmailConfirmationTokenAsync(appUser);
        
        var message = new Message([appUser.Email], "Email confirmation token", token, null);
        
        await emailService.SendEmail(message);

        return true;
    }
}