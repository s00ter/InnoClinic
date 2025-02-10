using InnoClinic.Application.IService;
using InnoClinic.Application.Models.Email;
using InnoClinic.Authorization.Dto.Account;
using InnoClinic.BusinessLogic.Contants;
using InnoClinic.BusinessLogic.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.Authorization.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController(
    UserManager<User> userManager,
    IEmailService emailService,
    ITokenService tokenService,
    IConfiguration configuration
    ) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegistrationDto userRegistrationDto)
    {
        var appUser = new User
        {
            FirstName = userRegistrationDto.FirstName,
            LastName = userRegistrationDto.LastName,
            UserName = userRegistrationDto.Email,
            Email = userRegistrationDto.Email,
        };
        
        var user = await userManager.FindByEmailAsync(appUser.Email);
        if (user != null)
        {
            return BadRequest("This Email is already in use");
        }
        
        var createResult = await userManager.CreateAsync(appUser, userRegistrationDto.Password!);
        if (!createResult.Succeeded)
        {
            var errors = createResult.Errors.Select(e => e.Description);
            return BadRequest(new RegistrationResponseDto { Errors = errors});
        }
        
        var roleResult = await userManager.AddToRoleAsync(appUser, RoleConstants.User);
        if (!roleResult.Succeeded)
        {
            var errors = roleResult.Errors.Select(e => e.Description);
            return BadRequest(new RegistrationResponseDto { Errors = errors});
        }
        
        var token = await userManager.GenerateEmailConfirmationTokenAsync(appUser);
        
        var message = new Message([appUser.Email], "Email confirmation token", token, null);
        
        await emailService.SendEmail(message);
        
        return Ok(new { Message = "Email confirmation token sent successfully" });
    }
    
    [HttpPost("email-verification")]
    public async Task<IActionResult> EmailVerification(string? email, string? token)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
        {
            return BadRequest("Invalid payload");
        }
        
        var user = await userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return BadRequest("Invalid email verification");
        }
        
        var isEmailVerified = await userManager.ConfirmEmailAsync(user, token);
        if (!isEmailVerified.Succeeded)
        {
            return BadRequest("Invalid email verification");
        }
        return Ok(new { Message = "Email confirmed" });
    }

    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate([FromBody] UserAuthenticationDto userAuthenticationDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var user = await userManager.FindByEmailAsync(userAuthenticationDto.Email!);
        if (user == null)
        {
            return Unauthorized(new AuthResponseDto { ErrorMessage = "Invalid Email" });
        }

        var isEmailConfirmed = await userManager.IsEmailConfirmedAsync(user);
        if (!isEmailConfirmed)
        {
            return Unauthorized(new AuthResponseDto { ErrorMessage = "Email not confirmed" });
        }
        
        var isCorrect = await userManager.CheckPasswordAsync(user, userAuthenticationDto.Password!);
        if (!isCorrect)
        {
            await userManager.AccessFailedAsync(user);
            
            return Unauthorized(new AuthResponseDto { ErrorMessage = "Invalid password" });
        }
        
        var userStatus = await userManager.IsLockedOutAsync(user);
        if (userStatus)
        {
            return Unauthorized(new AuthResponseDto{ErrorMessage = "User is locked out please try again later."});
        }
        
        await userManager.ResetAccessFailedCountAsync(user);

        var roles = await userManager.GetRolesAsync(user);
        var token = tokenService.CreateToken(user, roles);
        
        return Ok(new AuthResponseDto
        {
            IsAuthSuccessful = true,
            Token = token
        });
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var user  = await userManager.FindByEmailAsync(forgotPasswordDto.Email!);
        if (user is null)
        {
            return BadRequest(new { Message = "Invalid email address" });
        }
        
        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        
        var message = new Message([user.Email], "Reset password token", token, null);
        
        await emailService.SendEmail(message);
        
        return Ok(new { Message = "Password reset token sent successfully" });
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var user  = await userManager.FindByEmailAsync(resetPasswordDto.Email!);
        if (user is null)
        {
            return BadRequest(new { Message = "Invalid email address" });
        }
        
        var res = await userManager.ResetPasswordAsync(user, resetPasswordDto.Token!, resetPasswordDto.Password!);
        if (!res.Succeeded)
        {
            return BadRequest(res.Errors.Select(e => e.Description));
        }

        return Ok(new { Message = "Password change successfully" });
    }
    
    /*[HttpPost("signin-google")]
    public async Task<IActionResult> SignIn-Google()
    {
        var scopes = new[] { "https://www.googleapis.com/auth/cloud-platform.read-only", "https://www.googleapis.com/auth/youtube"};
        
        var credentials = GoogleWebAuthorizationBroker.AuthorizeAsync(
            new ClientSecrets
            {
                ClientId = configuration["Google:ClientId"],
                ClientSecret = configuration["Google:ClientId"],
            },
            scopes, "user", CancellationToken.None).Result;

        if (credentials.Token.IsExpired(SystemClock.Default))
            credentials.RefreshTokenAsync(CancellationToken.None).Wait();
        
    }*/
}