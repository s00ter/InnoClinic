using FluentValidation;
using InnoClinic.Application.Models.Email;
using InnoClinic.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using InnoClinic.Authorization.DependencyInjection;
using InnoClinic.Authorization.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddServices();
builder.Services.AddSwaggerSettings();
builder.Services.AddIdentitySettings();
builder.Services.AddJwtSettings();
builder.Services.AddMediatrSettings();

builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddDbContext<InnoClinicAuthContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("InnoClinicAuth")));

builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
    opt.TokenLifespan = TimeSpan.FromHours(builder.Configuration.GetValue<int>("TokenProviderOptions:TokenLifespanHours")));

builder.Services.Configure<EmailConfiguration>(builder.Configuration.GetSection("EmailConfiguration"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(opt => { });

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.MigrateDatabase();

app.Run();
