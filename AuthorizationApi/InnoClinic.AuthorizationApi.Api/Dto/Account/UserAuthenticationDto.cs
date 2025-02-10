using System.ComponentModel.DataAnnotations;

namespace InnoClinic.Authorization.Dto.Account;

public class UserAuthenticationDto
{
    [Required]
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; }
}