using Microsoft.AspNetCore.Mvc.Testing;
// ReSharper disable ClassNeverInstantiated.Global

namespace CandyBackend.Test;

public class ApplicationFixture
{
    public readonly WebApplicationFactory<Program> factory;

    public ApplicationFixture()
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
        if (Environment.GetEnvironmentVariable("ConnectionStrings__CandyDatabase") is null)
        {
            Environment.SetEnvironmentVariable("ConnectionStrings__CandyDatabase",
                "Host=127.0.0.1:5432;Database=test;Username=postgres;Password=postgres");
        }

        factory = new WebApplicationFactory<Program>();
    }
}
