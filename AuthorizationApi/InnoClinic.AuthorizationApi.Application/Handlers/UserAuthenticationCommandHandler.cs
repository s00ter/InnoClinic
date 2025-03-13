using Hellang.Middleware.ProblemDetails;
using InnoClinic.Application.Commands;
using InnoClinic.Application.IService;
using InnoClinic.BusinessLogic.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.Application.Handlers;

public class UserAuthenticationCommandHandler(
    UserManager<User> userManager,
    ITokenService tokenService
    ) 
    : IRequestHandler<UserAuthenticationCommand, string>
{
    public async Task<string> Handle(UserAuthenticationCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Request.Email)
                   ?? throw new ProblemDetailsException(new ProblemDetails() { Title = "Account does not exist" });

        var isEmailConfirmed = await userManager.IsEmailConfirmedAsync(user);
        if (!isEmailConfirmed)
        {
            throw new ProblemDetailsException(new ProblemDetails() { Title = "Email not confirmed" });
        }
        
        var isCorrect = await userManager.CheckPasswordAsync(user, request.Request.Password);
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
}