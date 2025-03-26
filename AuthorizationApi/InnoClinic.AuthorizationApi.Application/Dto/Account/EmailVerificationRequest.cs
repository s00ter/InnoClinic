namespace InnoClinic.Application.Dto.Account;

public class EmailVerificationRequest
{
    public string? Email { get; init; }
    public string? Token { get; init; }
}