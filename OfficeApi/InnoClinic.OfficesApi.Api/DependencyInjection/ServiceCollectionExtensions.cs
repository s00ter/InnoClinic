using InnoClinic.Office.BusinessLogic.Services.OfficeService;
using InnoClinic.Office.DataAccess.Models;
using InnoClinic.Office.DataAccess.Repositories.OfficeRepository;
using InnoClinic.Shared.Extensions;

namespace InnoClinic.Office.Api.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServicesSettings(
        this IServiceCollection services)
    {
        services.AddScoped<IOfficeService, OfficeService>();
        
        services.AddScoped<ICurrentUserInfo, CurrentUserInfo>();

        return services;
    }
    
    public static IServiceCollection AddRepositoriesSettings(
        this IServiceCollection services)
    {
        services.AddScoped<IOfficeRepository, OfficeRepository>();

        return services;
    }
    
    public static IServiceCollection AddOptionsSettings(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
        
        return services;
    }
}