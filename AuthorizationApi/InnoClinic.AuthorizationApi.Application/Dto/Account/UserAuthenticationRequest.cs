namespace InnoClinic.Application.Dto.Account;

public class UserAuthenticationRequest
{
    public string? Email { get; init; }
    public string? Password { get; init; }
}
