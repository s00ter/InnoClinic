namespace InnoClinic.Application.Dto.Account;

public class EmailVerificationRequest
{
    public string? Email { get; set; }
    public string? Token { get; set; }
}