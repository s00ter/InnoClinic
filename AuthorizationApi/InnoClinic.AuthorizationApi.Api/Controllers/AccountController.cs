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
        await sender.Send(new UserRegistrationCommand(request), cancellationToken);
        return Ok(new { Message = "Email confirmation token sent successfully" });
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
        await sender.Send(new UserAuthenticationCommand(request, HttpContext), cancellationToken);
        return Ok(new { Message = "Succeed authentication" });
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
    
    [HttpPost("token")]
    public async Task<IActionResult> RefreshAccessToken(CancellationToken cancellationToken = default)
    {
        await sender.Send(new RefreshAccessTokenCommand(HttpContext), cancellationToken);
        return Ok(new { Message = "Token change successfully" });
    }
}