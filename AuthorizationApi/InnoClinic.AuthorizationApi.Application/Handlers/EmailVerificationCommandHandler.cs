using Hellang.Middleware.ProblemDetails;
using InnoClinic.Application.Commands;
using InnoClinic.BusinessLogic.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.Application.Handlers;

public class EmailVerificationCommandHandler(
    UserManager<User> userManager
    ) 
    : IRequestHandler<EmailVerificationCommand>
{
    public async Task Handle(EmailVerificationCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Request.Email)
                   ?? throw new ProblemDetailsException(new ProblemDetails() { Title = "Account does not exist" });
        
        var isEmailVerified = await userManager.ConfirmEmailAsync(user, request.Request.Token);
        if (!isEmailVerified.Succeeded)
        {
            throw new ProblemDetailsException(new ProblemDetails() { Title = "Invalid email verification" });
        }
    }
}