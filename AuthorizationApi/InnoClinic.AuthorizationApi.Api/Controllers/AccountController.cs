using InnoClinic.Application.Dto.Account;
using InnoClinic.Application.IService;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.Authorization.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController(
    IAccountService accountService
    ) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request, CancellationToken cancellationToken = default)
    {
        var res = await accountService.Registration(request, cancellationToken);
        if (res)
        {
            return Ok(new { Message = "Email confirmation token sent successfully" });
        }
        return BadRequest(res);
    }
    
    [HttpPost("email-verification")]
    public async Task<IActionResult> EmailVerification([FromBody] EmailVerificationRequest request, CancellationToken cancellationToken = default)
    {
        await accountService.EmailVerification(request, cancellationToken);
        return Ok(new { Message = "Email confirmed" });
    }

    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate([FromBody] UserAuthenticationRequest request, CancellationToken cancellationToken = default)
    {
        var res = await accountService.Authenticate(request, cancellationToken);
        return Ok(res);
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request, CancellationToken cancellationToken = default)
    {
        await accountService.ForgotPassword(request, cancellationToken);
        return Ok(new { Message = "Password reset token sent successfully" });
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request, CancellationToken cancellationToken = default)
    {
        await accountService.ResetPassword(request, cancellationToken);
        return Ok(new { Message = "Password change successfully" });
    }
}