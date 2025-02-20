using System.Security.Cryptography;
using System.Text;
using InnoClinic.AppointmentApi.BL.Services.AppointmentService;
using InnoClinic.AppointmentApi.BL.Services.ResultService;
using InnoClinic.AppointmentApi.DataAccess.UnitOfWork;
using InnoClinic.Shared.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

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
    
    public static IServiceCollection AddSwagger(
        this IServiceCollection services)
    {
        services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    []
                }
            });
        });
        
        return services;
    }
    
    public static IServiceCollection AddJwtSettings(
        this IServiceCollection services)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = JwtConfiguration.Issuer,
                ValidateAudience = true,
                ValidAudience = JwtConfiguration.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(SHA256.HashData(Encoding.UTF8.GetBytes(JwtConfiguration.SigningKey))),
                ValidateLifetime = true
            };
        });

        return services;
    }
}