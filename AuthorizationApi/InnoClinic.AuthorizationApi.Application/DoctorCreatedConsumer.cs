using InnoClinic.BusinessLogic.Entities;
using InnoClinic.Shared.IventTypes;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace InnoClinic.Application;

public class DoctorCreatedConsumer(
    UserManager<User> userManager
    ) : IConsumer<DoctorCreated>
{
    public async Task Consume(ConsumeContext<DoctorCreated> context)
    {
        var user = await userManager.FindByIdAsync(context.Message.UserId.ToString())
            ?? throw new Exception("User not found");

       var res = await userManager.AddToRoleAsync(user, context.Message.Role);
       if (!res.Succeeded)
       {
           Console.WriteLine("Failed adding role: {0} to user: {1}, error: {2}",context.Message.Role, user.Id, string.Join(", ", res.Errors.Select(e => e.Description)));
       }
    }
}