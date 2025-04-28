using System.Security.Cryptography;
using System.Text;
using InnoClinic.Shared.Configurations;
using InnoClinic.Shared.Constants;
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
                    context.Token = context.Request.Cookies["Tung-tung-tung-sahur-cookies"];
                    return Task.CompletedTask;
                }
            };
        });
        
        return services;
    }
}