using InnoClinic.Application.Commands;
using InnoClinic.Application.IService;
using InnoClinic.BusinessLogic.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

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
                   ?? throw new Exception("Account does not exist");

        var isEmailConfirmed = await userManager.IsEmailConfirmedAsync(user);
        if (!isEmailConfirmed)
        {
            throw new Exception("Email not confirmed");
        }
        
        var isCorrect = await userManager.CheckPasswordAsync(user, request.Request.Password);
        if (!isCorrect)
        {
            await userManager.AccessFailedAsync(user);
            throw new Exception("Invalid password");
        }
        
        var userStatus = await userManager.IsLockedOutAsync(user);
        if (userStatus)
        {
            throw new Exception("User is locked out please try again later.");
        }
        
        await userManager.ResetAccessFailedCountAsync(user);

        var roles = await userManager.GetRolesAsync(user);
        var token = tokenService.CreateToken(user, roles);

        return token;
    }
}