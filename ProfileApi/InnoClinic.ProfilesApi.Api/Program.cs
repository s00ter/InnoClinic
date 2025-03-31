using InnoClinic.Prof.DataAccess;
using InnoClinic.ProfilesApi.DependencyInjection;
using InnoClinic.Shared.DependencyInjection;
using InnoClinic.Shared.Middlewares;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddDbContext<InnoClinicProfContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("InnoClinicProfile")));

builder.Services.AddServicesSettings();
builder.Services.AddRepositoriesSettings();
builder.Services.AddValidatorsSettings();

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

app.UseMiddleware<ValidationMiddleware>();

app.MapControllers();

await app.MigrateDatabase();

app.Run();
