using InnoClinic.AppointmentApi.DataAccess;
using Microsoft.EntityFrameworkCore;
using Hellang.Middleware.ProblemDetails;
using InnoClinic.AppointmentApi.Api.DependencyInjection;
using InnoClinic.AppointmentApi.Api.Middlewares;
using InnoClinic.AppointmentApi.BL.Exception;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwagger();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddDbContext<InnoClinicAppointmentContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("InnoClinicAppointments")));

builder.Services.AddServices();
builder.Services.AddRepositories();
builder.Services.AddJwtSettings();

builder.Services.AddProblemDetails(opt =>
{
    opt.ExceptionDetailsPropertyName = "Exception Details";
    opt.IncludeExceptionDetails = (ctx, ex) => builder.Environment.IsDevelopment() || builder.Environment.IsStaging();
    
    opt.Map<AppointmentServiceException>(exception => new ProblemDetails()
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

app.UseMiddleware<ParseAuthTokenMiddleware>();

await app.MigrateDatabase();

app.Run();
