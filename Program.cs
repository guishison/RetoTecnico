using Microsoft.EntityFrameworkCore;
using RetoTecnico.DataBase;
using Npgsql; // Importa el namespace 
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using RetoTecnico.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// 1. Connection String from Configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    // Fallback to environment variable (for Docker)
    connectionString = Environment.GetEnvironmentVariable("PG_CONNECTION_STRING");
    if (string.IsNullOrEmpty(connectionString))
    {
        throw new InvalidOperationException(
            "Connection string is missing.  Set it in appsettings.json or the PG_CONNECTION_STRING environment variable.");
    }
}

// 2. Use PostgreSQL with connection string
builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseNpgsql(connectionString));

// Register your services (AntiFraudService, KafkaProducerService) later
builder.Services.AddScoped<IAntiFraudService, AntiFraudService>();
// builder.Services.AddSingleton<IKafkaProducerService, KafkaProducerService>(); // Register Kafka producer

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IKafkaProducerService, KafkaProducerService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
