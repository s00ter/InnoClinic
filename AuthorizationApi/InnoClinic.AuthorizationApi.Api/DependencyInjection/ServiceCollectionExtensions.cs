using InnoClinic.Application;
using InnoClinic.Application.Behaviors;
using InnoClinic.Application.IService;
using InnoClinic.Application.Options;
using InnoClinic.Application.Service;
using InnoClinic.BusinessLogic.Entities;
using InnoClinic.DataAccess;
using InnoClinic.Shared.Extensions;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

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
    
    public static IServiceCollection AddMassTransitSettings(
        this IServiceCollection services)
    {
        var massTransitConfiguration = services.BuildServiceProvider().GetRequiredService<IOptions<MassTransitConfiguration>>().Value;
        var rabitMqConfiguration = services.BuildServiceProvider().GetRequiredService<IOptions<RabbitMqConfiguration>>().Value;
        
        services.AddMassTransit(x =>
        {
            x.AddConsumer<DoctorCreatedConsumer>();

            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(massTransitConfiguration.RabbitMqHost);

                cfg.ReceiveEndpoint(rabitMqConfiguration.CreateDoctorQueue, e =>
                {
                    e.ConfigureConsumer<DoctorCreatedConsumer>(ctx);
                });
            });
        });
        
        return services;
    }
    
    public static IServiceCollection AddOptionsSettings(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<EmailConfiguration>(configuration.GetSection("EmailConfiguration"));
        services.Configure<MassTransitConfiguration>(configuration.GetSection("MassTransitConfiguration"));
        services.Configure<RabbitMqConfiguration>(configuration.GetSection("RabbitMqConfiguration"));
        services.Configure<TokenConfiguration>(configuration.GetSection("TokenConfiguration"));
        
        services.Configure<DataProtectionTokenProviderOptions>(opt =>
            opt.TokenLifespan = TimeSpan.FromHours(configuration.GetValue<int>("TokenProviderOptions:TokenLifespanHours")));
        
        return services;
    }
}