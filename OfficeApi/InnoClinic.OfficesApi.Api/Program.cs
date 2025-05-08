using FluentValidation;
using InnoClinic.Office.Api.DependencyInjection;
using InnoClinic.Office.BusinessLogic.Validators;
using InnoClinic.Shared.DependencyInjection;
using InnoClinic.Shared.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssemblyContaining<CreateOfficeValidator>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

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