using InnoClinic.Prof.BusinessLogic.Services.DoctorService;
using InnoClinic.Prof.BusinessLogic.Services.PatientService;
using InnoClinic.Prof.BusinessLogic.Services.ReceptionistService;
using InnoClinic.Prof.BusinessLogic.Services.SpecializationService;
using InnoClinic.Prof.DataAccess;
using InnoClinic.Prof.DataAccess.Repositories.DoctorRepository;
using InnoClinic.Prof.DataAccess.Repositories.PatientRepository;
using InnoClinic.Prof.DataAccess.Repositories.ReceptionistRepository;
using InnoClinic.Prof.DataAccess.Repositories.SpecializationRepository;
using InnoClinic.Shared.Extensions;

namespace InnoClinic.ProfilesApi.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServicesSettings(
        this IServiceCollection services)
    {
        services.AddScoped<IDoctorService, DoctorService>();
        services.AddScoped<IPatientService, PatientService>();
        services.AddScoped<IReceptionistService, ReceptionistService>();
        services.AddScoped<ISpecializationService, SpecializationService>();
        
        services.AddScoped<ICurrentUserInfo, CurrentUserInfo>();
        
        services.AddSingleton<DapperContext>();

        return services;
    }
    
    public static IServiceCollection AddRepositoriesSettings(
        this IServiceCollection services)
    {
        services.AddScoped<IDoctorRepository, DoctorRepository>();
        services.AddScoped<IPatientRepository, PatientRepository>();
        services.AddScoped<IReceptionistRepository, ReceptionistRepository>();
        services.AddScoped<ISpecializationRepository, SpecializationRepository>();

        return services;
    }
}