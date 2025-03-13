using Hellang.Middleware.ProblemDetails;
using InnoClinic.Application.Commands;
using InnoClinic.Application.Dto.Account;
using InnoClinic.Application.IService;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Exception = System.Exception;

namespace InnoClinic.Authorization.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController(
    ISender sender
    ) 
    : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request, CancellationToken cancellationToken = default)
    {
        throw new NullReferenceException();
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
        var res = await sender.Send(new UserAuthenticationCommand(request), cancellationToken);
        return Ok(res);
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request, CancellationToken cancellationToken = default)
    {
        await sender.Send(new ForgotPasswordCommand(request), cancellationToken);
        return Ok(new { Message = "Password reset token sent successfully" });
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request, CancellationToken cancellationToken = default)
    {
        await sender.Send(new ResetPasswordCommand(request), cancellationToken);
        return Ok(new { Message = "Password change successfully" });
    }
}