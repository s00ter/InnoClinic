using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InnoClinic.ServiceApi.DataAccess;

public static class MigrationManager
{
    public static WebApplication MigrateDatabase(this WebApplication webApp)
    {
        using (var scope = webApp.Services.CreateScope())
        {
            using (var appContext = scope.ServiceProvider.GetRequiredService<InnoClinicServContext>())
            {
                try
                {
                    appContext.Database.Migrate();
                }
                catch (Exception e)
                {
                    throw;
                }
            }
        }

        return webApp;
    }
}