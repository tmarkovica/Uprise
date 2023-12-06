using Microsoft.EntityFrameworkCore;
using Serilog;
using Uprise.Authentication;
using Uprise.Repository.Power_Plant;
using Uprise.Repository.Uprise;
using Uprise.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("Bearer").AddJwtBearer();

builder.Services.AddAuthorization();

// Filters
builder.Services.AddScoped<UserAuthFilter>();

var connectionString = builder.Configuration.GetConnectionString("db-local");
// Db Contexts
builder.Services.AddDbContext<PowerPlantDbContext>(o => o.UseNpgsql(connectionString));
builder.Services.AddDbContext<UpriseDbContext>(o => o.UseNpgsql(connectionString));

// Services
builder.Services.AddScoped<PowerPlantService>();
builder.Services.AddScoped<PowerPlantProductionService>();
builder.Services.AddScoped<WeatherForecastService>();

// Serilog
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Serilog
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
