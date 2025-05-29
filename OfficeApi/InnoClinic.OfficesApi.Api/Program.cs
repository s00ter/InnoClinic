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
    
    app.UseSerilogRequestLogging();

    app.UseExceptionHandler(opt => { });

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    await app.StartAsync();
    
    foreach (var url in app.Urls)
    {
        Log.Information("Application started and listening on {Url}", url);
    }

    app.WaitForShutdown();
}
catch (Exception e)
{
    Log.Fatal(e, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}