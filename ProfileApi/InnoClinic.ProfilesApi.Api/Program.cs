using InnoClinic.Prof.DataAccess;
using InnoClinic.ProfilesApi.DependencyInjection;
using InnoClinic.Shared.DependencyInjection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddServicesSettings();
builder.Services.AddRepositoriesSettings();

builder.Services.AddPoliciesSettings();
builder.Services.AddJwtSettings();

builder.Services.AddDbContext<InnoClinicProfContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("InnoClinicProfile")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.MigrateDatabase();

app.Run();
