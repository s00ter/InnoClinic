using System.Text;
using InnoClinic.AppointmentApi.DataAccess;
using Microsoft.EntityFrameworkCore;
using Hellang.Middleware.ProblemDetails;
using InnoClinic.AppointmentApi.Api.DependencyInjection;
using InnoClinic.AppointmentApi.Api.Middlewares;
using InnoClinic.AppointmentApi.BL.Exception;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwagger();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddDbContext<InnoClinicAppointmentContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("InnoClinicAppointments")));

builder.Services.AddServices();
builder.Services.AddRepositories();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SigningKey"])),
        ValidateLifetime = true
    };
});

builder.Services.AddProblemDetails(opt =>
{
    opt.ExceptionDetailsPropertyName = "Exception Details";
    opt.IncludeExceptionDetails = (ctx, ex) => builder.Environment.IsDevelopment() || builder.Environment.IsStaging();
    
    opt.Map<CustomException>(exception => new ProblemDetails()
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
