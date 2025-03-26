using InnoClinic.Application.Commands;
using InnoClinic.BusinessLogic.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace InnoClinic.Application.Handlers;

public class EmailVerificationCommandHandler(
    UserManager<User> userManager
    ) 
    : IRequestHandler<EmailVerificationCommand,Unit>
{
    public async Task<Unit> Handle(EmailVerificationCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Request.Email)
                   ?? throw new Exception("Account does not exist");
        
        var isEmailVerified = await userManager.ConfirmEmailAsync(user, request.Request.Token);
        if (!isEmailVerified.Succeeded)
        {
            throw new Exception("Invalid email verification");
        }
        
        return Unit.Value;
    }
}