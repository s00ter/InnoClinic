using Azure.Storage.Blobs;
using InnoClinic.DocumentsApi.BL.Services.DocumentService;
using InnoClinic.DocumentsApi.BL.Services.PhotoService;

namespace InnoClinic.DocumentsApi.Api.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServicesSettings(
        this IServiceCollection services)
    {
        services.AddScoped<IDocumentService, DocumentService>();
        services.AddScoped<IPhotoService, PhotoService>();
        
        return services;
    }
    
    public static IServiceCollection AddDbSettings(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton(_ => new BlobServiceClient(configuration.GetConnectionString("BlobStorage")));

        return services;
    }
}