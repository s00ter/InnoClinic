using Microsoft.AspNetCore.Identity;

namespace InnoClinic.BusinessLogic.Entities;

public class User : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTimeOffset RefreshTokenExpiryTime { get; set; }
}