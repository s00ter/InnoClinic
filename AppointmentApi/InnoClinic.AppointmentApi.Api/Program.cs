using FluentValidation;
using InnoClinic.AppointmentApi.DataAccess;
using Microsoft.EntityFrameworkCore;
using InnoClinic.AppointmentApi.Api.DependencyInjection;
using InnoClinic.AppointmentApi.BL.Validators.AppointmentValidators;
using InnoClinic.Shared.DependencyInjection;
using InnoClinic.Shared.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssemblyContaining<CreateAppointmentValidator>();

builder.Services.AddDbContext<InnoClinicAppointmentContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("InnoClinicAppointments")));

builder.Services.AddServices();
builder.Services.AddRepositories();

builder.Services.AddControllersSettings();
builder.Services.AddJwtSettings();
builder.Services.AddPoliciesSettings();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

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
