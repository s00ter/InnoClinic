using InnoClinic.Application.Behaviors;
using InnoClinic.Application.IService;
using InnoClinic.Application.Service;
using InnoClinic.BusinessLogic.Entities;
using InnoClinic.DataAccess;
using InnoClinic.Shared.Extensions;
using Microsoft.AspNetCore.Identity;

namespace InnoClinic.Authorization.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(
        this IServiceCollection services)
    {
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<ITokenService, TokenService>();
        
        services.AddScoped<ICurrentUserInfo, CurrentUserInfo>();
        return services;
    }
    
    public static IServiceCollection AddIdentitySettings(
        this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 1;

                options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
                options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
            })
            .AddEntityFrameworkStores<InnoClinicAuthContext>()
            .AddDefaultTokenProviders();

        return services;
    }
    
    public static IServiceCollection AddMediatrSettings(
        this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        return services;
    }
}