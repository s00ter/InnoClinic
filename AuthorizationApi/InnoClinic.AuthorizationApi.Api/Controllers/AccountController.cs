using InnoClinic.Application.Commands;
using InnoClinic.Application.Dto.Account;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.Authorization.Controllers;

[Route("api/accounts")]
[ApiController]
public class AccountController(
    ISender sender
    ) 
    : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request, CancellationToken cancellationToken = default)
    {
        var res = await sender.Send(new UserRegistrationCommand(request), cancellationToken);
        if (res)
        {
            return Ok(new { Message = "Email confirmation token sent successfully" });
        }
        return BadRequest(res);
    }
    
    [HttpPost("email-verification")]
    public async Task<IActionResult> EmailVerification([FromBody] EmailVerificationRequest request, CancellationToken cancellationToken = default)
    {
        await sender.Send(new EmailVerificationCommand(request), cancellationToken);
        return Ok(new { Message = "Email confirmed" });
    }

    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate([FromBody] UserAuthenticationRequest request, CancellationToken cancellationToken = default)
    {
        var response = await sender.Send(new UserAuthenticationCommand(request), cancellationToken);
        HttpContext.Response.Cookies.Append("cookies",response.AccessToken);
        
        return Ok(response);
    }
    
    [HttpPost("forgot-password")]
    [Authorize]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request, CancellationToken cancellationToken = default)
    {
        await sender.Send(new ForgotPasswordCommand(request), cancellationToken);
        return Ok(new { Message = "Password reset token sent successfully" });
    }

    [HttpPost("reset-password")]
    [Authorize]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request, CancellationToken cancellationToken = default)
    {
        await sender.Send(new ResetPasswordCommand(request), cancellationToken);
        return Ok(new { Message = "Password change successfully" });
    }
    
    [HttpPost("refresh-access-token")]
    public async Task<IActionResult> RefreshAccessToken([FromBody] RefreshTokenRequest request , CancellationToken cancellationToken = default)
    {
        var response = await sender.Send(new RefreshAccessTokenCommand(request), cancellationToken);
        HttpContext.Response.Cookies.Append("cookies",response);
        
        return Ok(new { Message = "Token change successfully" });
    }
}