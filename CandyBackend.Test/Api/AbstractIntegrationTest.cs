using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;

namespace CandyBackend.Test.Api;

[FixtureLifeCycle(LifeCycle.SingleInstance)]
public abstract class AbstractIntegrationTest
{
    protected readonly WebApplicationFactory<Program> factory;

    protected AbstractIntegrationTest()
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
        if (Environment.GetEnvironmentVariable("ConnectionStrings__CandyDatabase") is null)
        {
            Environment.SetEnvironmentVariable("ConnectionStrings__CandyDatabase",
                "Host=127.0.0.1:5432;Database=postgres;Username=postgres;Password=postgres");
        }

        factory = new WebApplicationFactory<Program>();
    }
}