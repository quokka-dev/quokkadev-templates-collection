using FluentAssertions;
using Moq;
using Xunit;

namespace QuokkaDev.Package.Tests;

public class UnitTest1
{
    [Fact(DisplayName = "My test description")]
    public void Test1()
    {
        // Arrange
        var mock = new Mock<object>();

        // Act
        object o = mock.Object;

        // Assert
        o.Should().NotBeNull();
    }
}