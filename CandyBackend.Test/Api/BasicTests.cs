using System.Net;
using System.Net.Http.Json;
using CandyBackend.Api.Dto;
using CandyBackend.Core;
using CandyBackend.Repository.Candies;
using CandyBackend.Test.Core;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CandyBackend.Test.Api;

public class BasicTests : IClassFixture<ApplicationFixture>
{
    private readonly ApplicationFixture _fixture;

    public BasicTests(ApplicationFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task GetCandiesReturnsCorrectStatusCodeAndResult()
    {
        // Arrange
        Candy candy;
        using (var scope = _fixture.factory.Services.CreateScope())
        {
            var candyDao = scope.ServiceProvider.GetRequiredService<ICandyDao>();
            candy = candyDao.Save(ModelHelper.BuildCandy(0, "c1", 100));
        }

        var client = _fixture.factory.CreateClient();

        // Act
        var response = await client.GetAsync($"/candies/{candy.Id}");

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        response.Content.Headers.ContentType?.MediaType.Should().Be("application/json");
        var candyResponse = await response.Content.ReadFromJsonAsync<CandyOut>();
        candyResponse.Should().NotBeNull()
            .And.Match<CandyOut>(c => c.Id == candy.Id)
            .And.Match<CandyOut>(c => c.Name == "c1")
            .And.Match<CandyOut>(c => c.Price == 100);
    }

    [Fact]
    public async Task PostCandiesIsUnauthorized()
    {
        // Arrange
        var client = _fixture.factory.CreateClient();

        // Act
        var response = await client.PostAsync("/candies", null);
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}