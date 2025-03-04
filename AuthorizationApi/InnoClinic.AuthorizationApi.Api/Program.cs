using FluentValidation;
using InnoClinic.Application.Models.Email;
using InnoClinic.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hellang.Middleware.ProblemDetails;
using InnoClinic.Authorization.DependencyInjection;
using InnoClinic.Shared.Exceptions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddServices();
builder.Services.AddSwagger();
builder.Services.AddJwtSettings();
builder.Services.AddIdentitySettings();

builder.Services.AddDbContext<InnoClinicAuthContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("InnoClinicAuth")));

builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
    opt.TokenLifespan = TimeSpan.FromHours(2));

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("OnlyAdminUsers",
        policy => policy.RequireRole("Admin"));
});

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
