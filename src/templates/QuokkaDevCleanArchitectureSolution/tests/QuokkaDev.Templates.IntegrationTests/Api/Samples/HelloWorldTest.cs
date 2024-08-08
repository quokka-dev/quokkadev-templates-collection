using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using QuokkaDev.Templates.Application.Samples.Greetings;
using QuokkaDev.Templates.IntegrationTests.Fixtures;
using QuokkaDev.Templates.Persistence.Ef.Infrastructure.Implementations;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace QuokkaDev.Templates.IntegrationTests.Api.Samples
{
    [Collection("ApplicationFixture")]
    public class HelloWorldTest : IDisposable
    {
        private readonly ApplicationFixture<Program> _fixture;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IServiceScope _serviceScope;

        public HelloWorldTest(ApplicationFixture<Program> fixture)
        {
            this._fixture = fixture;
            _serviceScope = fixture.Services.CreateScope();
            _applicationDbContext = _serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            SeedTestData();
        }

        [Theory(DisplayName = "HelloWorld Should Respond Ok With Correct Message")]
        [InlineData("John Wick")]
        public async Task HelloWorld_Should_Respond_Ok_With_Correct_Message(string echoRequest)
        {
            // Arrange   
            using var client = _fixture.CreateClient();

            // Act
            var response = await client.GetAsync($"/api/v1.0/HelloWorld?name={echoRequest}");

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            string responseContent = await response.Content.ReadAsStringAsync();
            
            GreetingCommandResult? result = JsonSerializer.Deserialize<GreetingCommandResult>(responseContent, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            result.Should().NotBeNull();
            result?.GreetingMessage.Should().Contain(echoRequest);
        }

        public virtual void Dispose()
        {
            _applicationDbContext.Dispose();
            _serviceScope.Dispose();
        }

        private void SeedTestData()
        {
            // Seed test data
        }
    }
}