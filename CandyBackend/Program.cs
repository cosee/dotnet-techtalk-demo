using System.Diagnostics.CodeAnalysis;
using CandyBackend.Api;
using CandyBackend.Core;
using CandyBackend.Repository;
using CandyBackend.Repository.Candies;
using CandyBackend.Repository.Orders;
using EfSchemaCompare;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Serilog;
using Serilog.Events;
using ILogger = Microsoft.Extensions.Logging.ILogger;
// ReSharper disable InvertIf

namespace CandyBackend;

// ReSharper disable once ClassNeverInstantiated.Global
[SuppressMessage("Usage", "CA2254:Template should be a static expression")]
public class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .CreateLogger();

        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("CandyDatabase");

        if (connectionString is null)
        {
            throw new Exception("ConnectionString 'CandyDatabase' is missing.");
        }

        builder.Host.UseSerilog();

        var services = builder.Services;

        AutomapperConfiguration.Configure(services);

        // Add services to the container.
        services.AddScoped<ICandyService, CandyService>();
        services.AddScoped<ICandyDao, CandyDao>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IOrderDao, OrderDao>();

        // Add database context pool to the container.
        services.AddDbContextPool<ApplicationContext>(
            options => options
                .UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention()
        );

        services.AddHealthChecks()
            // Healthcheck for database: Microsoft.Extensions.Diagnostics.HealthChecks.EntityFrameworkCore
            .AddDbContextCheck<ApplicationContext>();
        services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        var app = builder.Build();

        InitDatabase(connectionString, app.Logger);
        VerifyDatabase(app.Services.CreateScope(), app.Logger);

        app.UseSerilogRequestLogging();

        // Configure the HTTP request pipeline.
        app.UseSwagger();
        app.UseSwaggerUI();
        app.MapHealthChecks("/health");

        app.UseAuthorization();

        app.MapControllers();

        app.ConfigureCustomExceptionMiddleware();
        
        app.Run();
    }

    private static void InitDatabase(string connectionString, ILogger appLogger)
    {
        var cnx = new NpgsqlConnection(connectionString);
        var evolve = new EvolveDb.Evolve(cnx, s => appLogger.LogInformation(s))
        {
            Locations = new[] { "Migrations" },
            IsEraseDisabled = true,
            MetadataTableName = "changelog"
        };
        evolve.Migrate();
    }

private static void VerifyDatabase(IServiceScope scope, ILogger appLogger)
{
    var applicationContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
    var comparer = new CompareEfSql();
    if (comparer.CompareEfWithDb(applicationContext))
    {
        appLogger.LogError(comparer.GetAllErrors);
        throw new Exception("Database schema not matching model classes.");
    }
}
}