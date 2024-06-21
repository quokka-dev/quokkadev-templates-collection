using QuokkaDev.AsyncNotifications.Abstractions;
using QuokkaDev.Templates.Application.Infrastructure.Interfaces;
using QuokkaDev.Templates.Domain.SeedWork;

namespace QuokkaDev.Templates.Application.Infrastructure.Services
{
    internal class DomainEventDispatcher : IDomainEventsDispatcher
    {
        private readonly IAsyncNotificationDispatcher notificationDispatcher;

        public DomainEventDispatcher(IAsyncNotificationDispatcher notificationDispatcher)
        {
            this.notificationDispatcher = notificationDispatcher;
        }

        public Task PublishAsync(IDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            return notificationDispatcher.DispatchAsync(domainEvent, cancellationToken);
        }
    }
}
