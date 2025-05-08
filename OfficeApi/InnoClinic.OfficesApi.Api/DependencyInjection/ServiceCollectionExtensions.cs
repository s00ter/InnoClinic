using InnoClinic.Office.BusinessLogic.Services.OfficeService;
using InnoClinic.Office.DataAccess;
using InnoClinic.Office.DataAccess.Models;
using InnoClinic.Office.DataAccess.Repositories.OfficeRepository;
using InnoClinic.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

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
    
    public static IServiceCollection AddDbSettings(
        this IServiceCollection services)
    {
        var mongoDbSettings = services.BuildServiceProvider().GetRequiredService<IOptions<MongoDbSettings>>().Value;
        
        services.AddDbContext<InnoClinicOffContext>(options =>
            options.UseMongoDB(mongoDbSettings.AtlasURI ?? "",mongoDbSettings.DatabaseName ?? ""));

        return services;
    }
}