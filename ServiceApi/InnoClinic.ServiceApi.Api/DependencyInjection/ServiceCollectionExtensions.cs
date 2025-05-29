using InnoClinic.ServiceApi.BusinessLogic.Services.ServiceCategoryService;
using InnoClinic.ServiceApi.BusinessLogic.Services.ServiceService;
using InnoClinic.ServiceApi.DataAccess;
using InnoClinic.ServiceApi.DataAccess.Repositories.ServiceCategoryRepository;
using InnoClinic.ServiceApi.DataAccess.Repositories.ServiceRepository;
using InnoClinic.Shared.Configurations;
using InnoClinic.Shared.Extensions;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.ServiceApi.Api.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(
        this IServiceCollection services)
    {
        services.AddScoped<IServiceService, ServiceService>();
        services.AddScoped<IServiceCategoryService, ServiceCategoryService>();
        
        services.AddScoped<ICurrentUserInfo, CurrentUserInfo>();
        
        return services;
    }
    
    public static IServiceCollection AddRepositories(
        this IServiceCollection services)
    {
        services.AddScoped<IServiceRepository, ServiceRepository>();
        services.AddScoped<IServiceCategoryRepository, ServiceCategoryRepository>();
        
        return services;
    }
    
    public static IServiceCollection AddDbSettings(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<InnoClinicServContext>(options => 
            options.UseSqlServer(configuration.GetConnectionString("InnoClinicServices")));
        
        return services;
    }
    
    public static IServiceCollection AddMassTransitSettings(
        this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(MassTransitConfiguration.Host);
            });
        });
        
        return services;
    }
}