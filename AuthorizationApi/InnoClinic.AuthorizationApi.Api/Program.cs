using System.Security.Cryptography;
using System.Text;
using FluentValidation;
using InnoClinic.Application.IService;
using InnoClinic.Application.Models.Email;
using InnoClinic.Application.Service;
using InnoClinic.BusinessLogic.Entities;
using InnoClinic.DataAccess;
using InnoClinic.Shared.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Hellang.Middleware.ProblemDetails;
using InnoClinic.Shared.Exceptions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(option =>
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


builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddDbContext<InnoClinicAuthContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("InnoClinicAuth")));

builder.Services.AddIdentity<User, IdentityRole>(options =>
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

builder.Services.AddAuthentication(options =>
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

builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
    opt.TokenLifespan = TimeSpan.FromHours(2));

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("OnlyAdminUsers",
        policy => policy.RequireRole("Admin"));
});

builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAccountService, AccountService>();

var emailConfig = builder.Configuration.GetSection("EmailConfiguration")
    .Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);

builder.Services.AddProblemDetails(opt =>
{
    opt.ExceptionDetailsPropertyName = "Exception Details";
    opt.IncludeExceptionDetails = (ctx, ex) => builder.Environment.IsDevelopment() || builder.Environment.IsStaging();
    
    opt.Map<AppException>(exception => new ProblemDetails()
    {
        Title = exception.Title,
        Detail = exception.Details,
        Status = StatusCodes.Status500InternalServerError,
        Type = exception.Type,
        Instance = exception.Instance
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseProblemDetails();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.MigrateDatabase();

app.Run();
