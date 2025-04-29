using FluentValidation;
using InnoClinic.Prof.BusinessLogic.Validators.DoctorValidators;
using InnoClinic.Prof.DataAccess;
using InnoClinic.ProfilesApi.DependencyInjection;
using InnoClinic.Shared.DependencyInjection;
using InnoClinic.Shared.Middlewares;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddValidatorsFromAssemblyContaining<RegistrationDoctorValidator>();

builder.Services.AddDbContext<InnoClinicProfContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("InnoClinicProfile")));

builder.Services.AddServicesSettings();
builder.Services.AddRepositoriesSettings();
builder.Services.AddControllersSettings();

builder.Services.AddJwtSettings();
builder.Services.AddPoliciesSettings();

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host("rabbitmq://localhost");
    });
});

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
