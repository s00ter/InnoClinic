using InnoClinic.Application.Commands;
using InnoClinic.Application.IService;
using InnoClinic.BusinessLogic.Entities;
using InnoClinic.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace InnoClinic.Application.Handlers;

public class RefreshAccessTokenCommandHandler(
    ICurrentUserInfo currentUserInfo,
    UserManager<User> userManager,
    ITokenService tokenService
    ) : IRequestHandler<RefreshAccessTokenCommand, string>
{
    public async Task<string> Handle(RefreshAccessTokenCommand request, CancellationToken cancellationToken)
    {
        var userId = currentUserInfo.GetUserId();
        
        var user = await userManager.FindByIdAsync(userId) 
                   ?? throw new Exception("User not found");

        if (user.RefreshToken != request.Request.RefreshToken || user.RefreshTokenExpiryTime < DateTime.Now)
        {
            throw new Exception("Invalid refresh token");
        }
        
        var roles = await userManager.GetRolesAsync(user);

        return tokenService.CreateToken(user, roles);
    }
}