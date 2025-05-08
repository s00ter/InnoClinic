using InnoClinic.BusinessLogic.Entities;
using InnoClinic.Shared.IventTypes;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace InnoClinic.Application;

public class DoctorCreatedConsumer(
    UserManager<User> userManager,
    ILogger<DoctorCreatedConsumer> logger
    ) : IConsumer<DoctorCreated>
{
    public async Task Consume(ConsumeContext<DoctorCreated> context)
    {
        var user = await userManager.FindByIdAsync(context.Message.UserId.ToString())
            ?? throw new Exception("User not found");

       var res = await userManager.AddToRoleAsync(user, context.Message.Role);
       if (!res.Succeeded)
       {
           logger.LogError("Failed adding role: {role} to user: {userId}, error: {error}",context.Message.Role, user.Id, string.Join(", ", res.Errors.Select(e => e.Description)));
           
       }
    }
}