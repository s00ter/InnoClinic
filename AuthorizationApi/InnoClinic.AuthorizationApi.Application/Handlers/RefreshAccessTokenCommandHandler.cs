using InnoClinic.Application.Commands;
using InnoClinic.Application.IService;
using InnoClinic.Application.Options;
using InnoClinic.BusinessLogic.Entities;
using InnoClinic.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace InnoClinic.Application.Handlers;

public class RefreshAccessTokenCommandHandler(
    ICurrentUserInfo currentUserInfo,
    UserManager<User> userManager,
    ITokenService tokenService,
    IOptions<TokenConfiguration> options
    ) : IRequestHandler<RefreshAccessTokenCommand, Unit>
{
    public async Task<Unit> Handle(RefreshAccessTokenCommand request, CancellationToken cancellationToken)
    {
        var userId = currentUserInfo.GetUserId();
        
        var user = await userManager.FindByIdAsync(userId) 
                   ?? throw new Exception("User not found");
        
        request.Context.Request.Cookies.TryGetValue(options.Value.RefreshToken,out var refreshToken);
        
        if (user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime < DateTimeOffset.Now)
        {
            throw new Exception("Invalid refresh token");
        }
        
        var roles = await userManager.GetRolesAsync(user);

        var token = tokenService.CreateToken(user, roles);
        
        request.Context.Response.Cookies.Append(options.Value.AccessToken, token, 
            new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddHours(8),
                HttpOnly = true,
                IsEssential = true,
                Secure = true,
                SameSite = SameSiteMode.None
            });

        return Unit.Value;
    }
}