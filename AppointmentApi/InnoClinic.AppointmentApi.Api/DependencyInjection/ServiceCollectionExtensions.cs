using FluentValidation;
using InnoClinic.AppointmentApi.BL.Dto.AppointmentDto;
using InnoClinic.AppointmentApi.BL.Dto.ResultDto;
using InnoClinic.AppointmentApi.BL.Services.AppointmentService;
using InnoClinic.AppointmentApi.BL.Services.ResultService;
using InnoClinic.AppointmentApi.BL.Validators.AppointmentValidators;
using InnoClinic.AppointmentApi.BL.Validators.ResultValidators;
using InnoClinic.AppointmentApi.DataAccess.UnitOfWork;

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
    
    public static IServiceCollection AddValidatorsSettings(
        this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateAppointmentRequest>, CreateAppointmentValidator>();
        services.AddScoped<IValidator<UpdateAppointmentRequest>, UpdateAppointmentValidator>();
        
        services.AddScoped<IValidator<CreateResultRequest>, CreateResultValidator>();
        services.AddScoped<IValidator<UpdateResultRequest>, UpdateResultValidator>();

        return services;
    }
}