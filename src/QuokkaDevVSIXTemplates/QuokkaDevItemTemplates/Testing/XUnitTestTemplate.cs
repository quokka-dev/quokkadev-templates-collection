using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Xunit;

namespace $rootnamespace$
{
    public class $safeitemname$
    {
        public $safeitemname$()
        {
        }

        [Fact(DisplayName = "Test")]
        public void Test1()
        {
            // Arrange
            var mock = new Mock<object>();
            mock.Setup(m => m.Equals(It.IsAny<object>())).Returns(true);
            var obj = mock.Object;

            // Act
            var obj = mock.Object;

            // Assert
            obj.Should().NotBeNull();
        }

        [Fact(DisplayName = "Test2")]
        public async Task Test2()
        {
            // Arrange
            var mock = new Mock<object>();
            mock.Setup(m => m.Equals(It.IsAny<object>())).Returns(true);
            var obj = mock.Object;

            // Act
            await Task.CompletedTask;

            // Assert
            obj.Should().NotBeNull();
        }
    }
}
