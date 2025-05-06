using FluentValidation;
using InnoClinic.DataAccess;
using Microsoft.EntityFrameworkCore;
using InnoClinic.Authorization.DependencyInjection;
using InnoClinic.Shared.DependencyInjection;
using InnoClinic.Shared.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddServices();
builder.Services.AddIdentitySettings();
builder.Services.AddMediatrSettings();
builder.Services.AddOptionsSettings(builder.Configuration);
builder.Services.AddMassTransitSettings();

builder.Services.AddJwtSettings();
builder.Services.AddPoliciesSettings();

builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddDbContext<InnoClinicAuthContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("InnoClinicAuth")));

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
