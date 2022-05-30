using FluentAssertions;
using QuokkaDev.Templates.Api;
using QuokkaDev.Templates.Application.UseCases.Ping.Queries;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace QuokkaDev.Templates.IntegrationTests;

public class IntegrationTest1
{
    private readonly WebApplicationFactory<Program> factory;
    private readonly JsonSerializerOptions options;

    public IntegrationTest1()
    {
        factory = new WebApplicationFactory<Program>()
        .WithWebHostBuilder(_ =>
        {
            // ... Configure test services            
        });

        options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    [Theory]
    [InlineData("ECHO!!!")]
    [InlineData("My request test")]
    public async Task Ping_Should_Contain_Response_Text(string echoRequest)
    {
        // Arrange   
        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync($"/api/v1.0/ping?echoRequest={echoRequest}");
        var contentString = await response.Content.ReadAsStringAsync();
        var objectResponse = JsonSerializer.Deserialize<PingQueryResult>(contentString, options);

        // Assert
        objectResponse?.EchoResponse.Should().StartWith($"Response: {echoRequest}");
    }

    [Theory]
    [InlineData("ECHO!!!")]
    [InlineData("My request test")]
    public async Task Ping_Should_Respond_Ok(string echoRequest)
    {
        // Arrange   
        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync($"/api/v1.0/ping?echoRequest={echoRequest}");

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }

    [Fact]
    public async Task Empty_Ping_Request_Should_Respond_BadRequest()
    {
        // Arrange   
        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/v1.0/ping?echoRequest=");

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }
}