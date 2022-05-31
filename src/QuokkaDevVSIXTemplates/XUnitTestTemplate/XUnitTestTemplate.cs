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

        [Fact]
        public async Task Test1()
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
