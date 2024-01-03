using QuokkaDev.AsyncNotifications.Abstractions;
using QuokkaDev.Templates.Application.Services;
using QuokkaDev.Templates.Domain.SeedWork;

namespace QuokkaDev.Templates.Persistence.Ef.Services
{
    internal class DomainEventDispatcher : IDomainEventsDispatcher
    {
        private readonly IAsyncNotificationDispatcher notificationDispatcher;

        public DomainEventDispatcher(IAsyncNotificationDispatcher notificationDispatcher)
        {
            this.notificationDispatcher = notificationDispatcher;
        }

        public Task Publish(IDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            return notificationDispatcher.DispatchAsync(domainEvent, cancellationToken);
        }
    }
}
