using QuokkaDev.AsyncNotifications.Abstractions;
using QuokkaDev.Templates.Domain.Interfaces;
using QuokkaDev.Templates.Domain.SeedWork;

namespace QuokkaDev.Templates.DataAccess.Commands.Services
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
        public Task Notify(IDomainEventNotification<IDomainEvent> domainEventNotifications, CancellationToken cancellationToken)
        {
            return notificationDispatcher.DispatchAsync(domainEventNotifications, cancellationToken);
        }
    }
}
