using InnoClinic.BusinessLogic.Entities;
using InnoClinic.Shared.IventTypes;
using MassTransit;
using Microsoft.AspNetCore.Identity;

namespace InnoClinic.Application;

public class DoctorCreatedConsumer(
    UserManager<User> userManager
    ) : IConsumer<IDoctorCreated>
{
    public async Task Consume(ConsumeContext<IDoctorCreated> context)
    {
        var user = await userManager.FindByIdAsync(context.Message.UserId.ToString())
            ?? throw new Exception("User not found");
        
        await userManager.AddToRoleAsync(user, context.Message.Role);
        
        await Task.CompletedTask;
    }
}