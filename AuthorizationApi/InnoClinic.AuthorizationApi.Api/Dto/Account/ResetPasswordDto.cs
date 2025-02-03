using System.ComponentModel.DataAnnotations;

namespace InnoClinic.Authorization.Dto.Account;

public class ResetPasswordDto
{
    [Required]
    public string? Password { get; set; }
    
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string? ConfirmPassword { get; set; }

    public string? Email { get; set; }
    public string? Token { get; set; }
}