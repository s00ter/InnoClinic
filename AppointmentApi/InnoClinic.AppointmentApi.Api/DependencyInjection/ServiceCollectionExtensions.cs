using System.Text.Json.Serialization;
using InnoClinic.AppointmentApi.BL.Services.AppointmentService;
using InnoClinic.AppointmentApi.BL.Services.ResultService;
using InnoClinic.AppointmentApi.DataAccess.UnitOfWork;
using InnoClinic.Shared.Filters;
using DateTimeOffsetConverter = InnoClinic.Shared.Extensions.DateTimeOffsetConverter;
using GuidConverter = InnoClinic.Shared.Extensions.GuidConverter;

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
    
    public static IServiceCollection AddControllerSettings(
        this IServiceCollection services)
    {
        services.AddControllers(options =>
            {
                options.Filters.Add<GlobalValidationFilter>();
            })
            .AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                
                x.JsonSerializerOptions.Converters.Add(new DateTimeOffsetConverter());
                x.JsonSerializerOptions.Converters.Add(new GuidConverter());
            });
                

        return services;
    }
}