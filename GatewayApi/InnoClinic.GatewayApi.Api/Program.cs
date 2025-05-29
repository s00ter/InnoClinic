using InnoClinic.Shared.DependencyInjection;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Polly;
using Serilog;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

try
{
    Log.Information("Starting application");

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.ConfigureSerilog();

    builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
    builder.Services.AddOcelot(builder.Configuration).AddCacheManager(
        x =>
        {
            x.WithDictionaryHandle();
        })
        .AddPolly();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddControllers();

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

    await app.UseOcelot();
    
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