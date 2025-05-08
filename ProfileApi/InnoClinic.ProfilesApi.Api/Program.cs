using FluentValidation;
using InnoClinic.Prof.BusinessLogic.Validators.DoctorValidators;
using InnoClinic.Prof.DataAccess;
using InnoClinic.ProfilesApi.DependencyInjection;
using InnoClinic.Shared.DependencyInjection;
using InnoClinic.Shared.Middlewares;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

try
{
    Log.Information("Starting application");

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.ConfigureSerilog();

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

    builder.Services.AddMassTransit(x => { x.UsingRabbitMq((ctx, cfg) => { cfg.Host("rabbitmq://localhost"); }); });

    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
    builder.Services.AddProblemDetails();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseSerilogRequestLogging();

    app.UseExceptionHandler(opt => { });

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    await app.MigrateDatabase();

    app.Run();

}
catch (Exception e)
{
    Log.Fatal(e, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}