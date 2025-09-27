using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using PortfolioApi.Entities;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace PortfolioApi.Tests;

public class IntegrationTests : IClassFixture<CustomWebApplicationFactory>
{

    private readonly CustomWebApplicationFactory _factory;

    public IntegrationTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task TestEndpointSaveFormExpectSuccess()
    {

        // Arrange
        var client = _factory.CreateClient();

        var message = new Message
        {
            SenderName = "Test",
            SenderEmail = "test@email.com",
            Content = "This is a test"
        };

        // Act
        var response = await client.PostAsJsonAsync<Message>("/api/savemessage", message);
        // Assert
        response.EnsureSuccessStatusCode();
    }
    [Fact]
    public async Task TestEndpointSaveFormExpect400()
    {
        // Arrange
        var client = _factory.CreateClient();

        var message = new Message
        {
            SenderName = "Test",
            SenderEmail = "test@email.com",
        };

        // Act
        var response = await client.PostAsJsonAsync<Message>("/api/savemessage", message);
        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
