using FluentValidation;
using InnoClinic.Office.Api.DependencyInjection;
using InnoClinic.Office.BusinessLogic.Validators;
using InnoClinic.Shared.DependencyInjection;
using InnoClinic.Shared.Middlewares;
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
    
    builder.Services.AddValidatorsFromAssemblyContaining<CreateOfficeValidator>();

    builder.Services.AddDbSettings();
    builder.Services.AddRepositoriesSettings();
    builder.Services.AddServicesSettings();
    builder.Services.AddOptionsSettings(builder.Configuration);

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