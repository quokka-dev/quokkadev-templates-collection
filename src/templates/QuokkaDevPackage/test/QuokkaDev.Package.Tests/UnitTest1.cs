using FluentAssertions;
using Moq;
using Xunit;

namespace QuokkaDev.Package.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        // Arrange
        var mock = new Mock<object>();

        // Act
        object o = mock.Object;

        // Assert
        o.Should().NotBeNull();
    }

    [Fact]
    public static void Test()
    {
        // Method intentionally left empty.
    }
}