using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace InnoClinic.Shared.DependencyInjection;

public static class ApplicationExtension
{
    public static void ConfigureSerilog(this IHostBuilder host)
    {
        host.UseSerilog((ctx, lc) =>
        {
            lc.WriteTo.Console();
        });
    }
}