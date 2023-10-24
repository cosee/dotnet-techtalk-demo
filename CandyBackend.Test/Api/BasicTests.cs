using NUnit.Framework;

namespace CandyBackend.Test.Api;

public class BasicTests : AbstractIntegrationTest
{
    [DatapointSource]
    private readonly string[] _urls = { "/candies", "/orders" };

    [Theory]
    public async Task GetEndpointsReturnSuccessAndCorrectContentType(string url)
    {
        // Arrange
        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync(url);

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        Assert.That(response.Content.Headers.ContentType?.MediaType, Is.EqualTo("application/json"));
    }
}