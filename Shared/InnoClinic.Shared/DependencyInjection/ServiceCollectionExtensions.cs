using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using InnoClinic.Shared.Configurations;
using InnoClinic.Shared.Constants;
using InnoClinic.Shared.Extensions;
using InnoClinic.Shared.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace InnoClinic.Shared.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPoliciesSettings(
        this IServiceCollection services)
    {
        services.AddAuthorization(options =>
            {
                options.AddPolicy("OnlyForMembers", policy =>
                    policy.RequireRole(RoleConstants.Admin, RoleConstants.Doctor, RoleConstants.Patient));
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
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    context.Token = context.Request.Cookies[TokenConstants.AccessToken];
                    return Task.CompletedTask;
                }
            };
        });

        return services;
    }
    
    public static IServiceCollection AddControllersSettings(
        this IServiceCollection services)
    {
        services.AddControllers(options =>
            {
                options.Filters.Add<GlobalValidationFilter>();
            })
            .AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                
                x.JsonSerializerOptions.Converters.Add(new DateTimeOffsetConverter());
                x.JsonSerializerOptions.Converters.Add(new GuidConverter());
            });
        
        return services;
    }
}