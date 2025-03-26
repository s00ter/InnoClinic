namespace InnoClinic.Application.Dto.Account;

public class ResetPasswordRequest
{
    public string? Password { get; init; }
    public string? ConfirmPassword { get; init; }
    public string? Email { get; init; }
    public string? Token { get; init; }
}