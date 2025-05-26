using InnoClinic.AppointmentApi.BL.Services.AppointmentService;
using InnoClinic.AppointmentApi.BL.Services.ResultService;
using InnoClinic.AppointmentApi.DataAccess;
using InnoClinic.AppointmentApi.DataAccess.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.AppointmentApi.Api.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(
        this IServiceCollection services)
    {
        services.AddScoped<IResultService, ResultService>();
        services.AddScoped<IAppointmentService, AppointmentService>();

        return services;
    }
    
    public static IServiceCollection AddRepositories(
        this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
    
    public static IServiceCollection AddDbSettings(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<InnoClinicAppointmentContext>(options => 
            options.UseNpgsql(configuration.GetConnectionString("InnoClinicAppointments")));

        return services;
    }
}