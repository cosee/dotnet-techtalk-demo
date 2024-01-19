using System.Text.Json.Serialization;
using CandyBackend.Api;
using CandyBackend.Core;
using CandyBackend.Repository;
using CandyBackend.Repository.Candies;
using CandyBackend.Repository.Orders;
using EfSchemaCompare;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using Serilog;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace CandyBackend;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("CandyDatabase");

        if (connectionString is null)
        {
            throw new Exception("ConnectionString 'CandyDatabase' is missing.");
        }

        builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
            loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));

        var services = builder.Services;

        services.ConfigureAutoMapper();

        var otel = services.AddOpenTelemetry();
        // Configure OpenTelemetry Resources with the application name
        otel.ConfigureResource(resourceBuilder => resourceBuilder.AddService(serviceName: builder.Environment.ApplicationName));
        // Add Metrics for ASP.NET Core and our custom metrics and export to Prometheus
        otel.WithMetrics(metrics => metrics
            .AddAspNetCoreInstrumentation()
            .AddMeter("Microsoft.AspNetCore.Hosting")
            .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
            .AddPrometheusExporter());


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
        services.AddControllers()
            .AddJsonOptions(configure => { configure.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.ConfigureSwagger();
        services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = "Basic";
                option.DefaultChallengeScheme = "Basic";
            })
            .AddScheme<BasicAuthenticationOptions, BasicAuthenticationHandler>("Basic", _ => { });

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

        // Configure the Prometheus scraping endpoint
        app.MapPrometheusScrapingEndpoint();

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