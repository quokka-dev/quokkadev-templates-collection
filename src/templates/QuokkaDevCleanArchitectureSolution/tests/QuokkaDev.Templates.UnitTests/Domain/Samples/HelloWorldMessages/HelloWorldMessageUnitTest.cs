using FluentAssertions;
using Moq;
using QuokkaDev.Templates.Domain.Samples.HelloWorldMessages;
using System.Threading.Tasks;
using Xunit;

namespace QuokkaDev.Templates.UnitTests.Domain.Samples.HelloWorldMessages;

public class HelloWorldMessageUnitTest
{
    [Theory(DisplayName = "HelloWorldMessage Should Be Created As Expected")]
    [InlineData("Hello World!")]
    public void HelloWorldMessageShouldBeCreatedAsExpected(string message)
    {
        // Arrange

        // Act
        var helloWorld = HelloWorldMessageFactory.CreateNewHelloWorldMessage(message);

        // Assert
        helloWorld.Should().NotBeNull();
        helloWorld.Message.Should().Be(message);
    }
}