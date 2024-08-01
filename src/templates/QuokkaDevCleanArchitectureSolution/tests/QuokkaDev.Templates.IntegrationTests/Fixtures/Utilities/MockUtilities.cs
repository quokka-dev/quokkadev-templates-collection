using Moq;
using QuokkaDev.Templates.Application.Infrastructure.Interfaces;
using QuokkaDev.Templates.Domain.Common.Keys;
using QuokkaDev.Templates.Domain.SeedWork;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QuokkaDev.Templates.IntegrationTests.Fixtures.Utilities
{
    internal static class MockUtilities
    {
        public static Mock<IDomainEventsDispatcher> GetIDomainEventsDispatcherMock()
        {

            var mock = new Mock<IDomainEventsDispatcher>();
            mock.Setup(m => m.PublishAsync(It.IsAny<IDomainEvent>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
            return mock;
        }

        public static Mock<ICurrentUserAccessor> GetICurrentUserAccessorMock()
        {
            var mock = new Mock<ICurrentUserAccessor>();
            mock.Setup(m => m.GetCurrentUserNameAsync()).ReturnsAsync("IntegrationTestUser");
            return mock;
        }
    }

    internal static class MockConstants
    {
        //Define here the message ids for the items that are used in the tests
        public static readonly HelloWorldMessageId HELLO_WORLD_MESSAGE_ID = (HelloWorldMessageId)new Guid("1d71d26d-c327-4dfd-a084-6ab91e0bca33");
    }
}
