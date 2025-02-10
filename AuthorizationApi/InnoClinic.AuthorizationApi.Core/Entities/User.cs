using Microsoft.AspNetCore.Identity;

namespace InnoClinic.BusinessLogic.Entities;

public class User : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}