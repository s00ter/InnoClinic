using InnoClinic.Application.Commands;
using InnoClinic.Application.Dto.Account;
using InnoClinic.Application.IService;
using InnoClinic.Application.Options;
using InnoClinic.BusinessLogic.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace InnoClinic.Application.Handlers;

public class UserAuthenticationCommandHandler(
    UserManager<User> userManager,
    ITokenService tokenService,
    IOptions<TokenConfiguration> options
    ) 
    : IRequestHandler<UserAuthenticationCommand, Unit>
{
    public async Task<Unit> Handle(UserAuthenticationCommand request, CancellationToken cancellationToken)
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
        
        var tokenDto = new TokenResponse
        {
            AccessToken = tokenService.CreateToken(user, roles),
            RefreshToken = tokenService.CreateRefreshToken()
        };
        
        user.RefreshToken = tokenDto.RefreshToken;
        user.RefreshTokenExpiryTime = DateTimeOffset.Now.AddDays(7);
        
        await userManager.UpdateAsync(user);
        
        request.Context.Response.Cookies.Append(options.Value.AccessToken, tokenDto.AccessToken, 
            new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddHours(8),
                HttpOnly = true,
                IsEssential = true,
                Secure = true,
                SameSite = SameSiteMode.None
            });
        
        request.Context.Response.Cookies.Append(options.Value.RefreshToken, tokenDto.RefreshToken, 
            new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddDays(7),
                HttpOnly = true,
                IsEssential = true,
                Secure = true,
                SameSite = SameSiteMode.None
            });

        return Unit.Value;
    }
}