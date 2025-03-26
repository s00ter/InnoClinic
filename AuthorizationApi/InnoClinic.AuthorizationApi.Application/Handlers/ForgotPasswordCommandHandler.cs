using InnoClinic.Application.Commands;
using InnoClinic.Application.IService;
using InnoClinic.Application.Models.Email;
using InnoClinic.BusinessLogic.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace InnoClinic.Application.Handlers;

public class ForgotPasswordCommandHandler(
    UserManager<User> userManager,
    IEmailService emailService
    ) 
    : IRequestHandler<ForgotPasswordCommand,Unit>
{
    public async Task<Unit> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Request.Email)
                   ?? throw new Exception("Account does not exist");
        
        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        
        var message = new Message([user.Email], "Reset password token", token, null);
        
        await emailService.SendEmail(message);
        
        return Unit.Value;
    }
}