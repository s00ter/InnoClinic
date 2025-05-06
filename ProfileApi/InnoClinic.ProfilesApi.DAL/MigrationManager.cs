using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InnoClinic.Prof.DataAccess;

public static class MigrationManager
{
    public static async Task MigrateDatabase(this WebApplication webApp)
    {
        using var scope = webApp.Services.CreateScope();
        await using var appContext = scope.ServiceProvider.GetRequiredService<InnoClinicProfContext>();
        await appContext.Database.MigrateAsync();
    }
}