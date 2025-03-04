using System.Text.Json.Serialization;
using FluentValidation;
using InnoClinic.AppointmentApi.DataAccess;
using Microsoft.EntityFrameworkCore;
using Hellang.Middleware.ProblemDetails;
using InnoClinic.AppointmentApi.Api.DependencyInjection;
using InnoClinic.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwagger();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers()
    .AddJsonOptions(x => 
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddDbContext<InnoClinicAppointmentContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("InnoClinicAppointments")));

builder.Services.AddServices();
builder.Services.AddRepositories();
builder.Services.AddJwtSettings();

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

builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

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
