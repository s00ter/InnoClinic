using InnoClinic.Application.Dto.Account;

namespace InnoClinic.Application.IService;

public interface IAccountService
{ 
    Task<bool> Registration(UserRegistrationRequest request, CancellationToken cancellationToken);
    Task EmailVerification(EmailVerificationRequest request, CancellationToken cancellationToken);
    Task<string> Authenticate(UserAuthenticationRequest request, CancellationToken cancellationToken);
    Task ForgotPassword(ForgotPasswordRequest request, CancellationToken cancellationToken);
    Task ResetPassword(ResetPasswordRequest request, CancellationToken cancellationToken);
}