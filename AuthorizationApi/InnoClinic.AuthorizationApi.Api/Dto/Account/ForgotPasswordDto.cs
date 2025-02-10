using System.ComponentModel.DataAnnotations;

namespace InnoClinic.Authorization.Dto.Account;

public class ForgotPasswordDto
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
}