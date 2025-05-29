using InnoClinic.Application;
using InnoClinic.Application.Behaviors;
using InnoClinic.Application.IService;
using InnoClinic.Application.Options;
using InnoClinic.Application.Service;
using InnoClinic.BusinessLogic.Entities;
using InnoClinic.DataAccess;
using InnoClinic.Shared.Configurations;
using InnoClinic.Shared.Constants;
using InnoClinic.Shared.Extensions;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Authorization.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServicesSettings(
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
        services.AddMassTransit(x =>
        {
            x.AddConsumer<AddRoleConsumer>();

            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(MassTransitConfiguration.Host);

                cfg.ReceiveEndpoint(RabbitMqQueues.AddRoleQueue, e =>
                {
                    e.ConfigureConsumer<AddRoleConsumer>(ctx);
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
        
        services.Configure<DataProtectionTokenProviderOptions>(opt =>
            opt.TokenLifespan = TimeSpan.FromHours(configuration.GetValue<int>("TokenProviderOptions:TokenLifespanHours")));
        
        return services;
    }
    
    public static IServiceCollection AddDbSettings(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<InnoClinicAuthContext>(options => 
            options.UseSqlServer(configuration.GetConnectionString("InnoClinicAuth")));

        return services;
    }
}