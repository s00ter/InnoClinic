using InnoClinic.Application.Commands;
using InnoClinic.BusinessLogic.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Exception = System.Exception;

namespace InnoClinic.Application.Handlers;

public class ResetPasswordCommandHandler(
    UserManager<User> userManager
    ) 
    : IRequestHandler<ResetPasswordCommand,Unit>
{
    public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Request.Email)
                   ?? throw new Exception("Account does not exist");
        
        var res = await userManager.ResetPasswordAsync(user, request.Request.Token, request.Request.Password);
        if (!res.Succeeded)
        {
            throw new Exception("Reset password failed");
        }
        
        return Unit.Value;
    }
}