using Hellang.Middleware.ProblemDetails;
using InnoClinic.Application.Commands;
using InnoClinic.BusinessLogic.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.Application.Handlers;

public class ResetPasswordCommandHandler(
    UserManager<User> userManager
    ) 
    : IRequestHandler<ResetPasswordCommand,Unit>
{
    public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Request.Email)
                   ?? throw new ProblemDetailsException(new ProblemDetails() { Title = "Account does not exist" });
        
        var res = await userManager.ResetPasswordAsync(user, request.Request.Token, request.Request.Password);
        if (!res.Succeeded)
        {
            throw new ProblemDetailsException(new ProblemDetails() { Title = "Reset password failed" });
        }
        
        return Unit.Value;
    }
}