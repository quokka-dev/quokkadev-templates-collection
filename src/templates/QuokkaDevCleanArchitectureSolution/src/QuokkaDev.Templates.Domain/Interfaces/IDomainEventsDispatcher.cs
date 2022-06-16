using QuokkaDev.Templates.Domain.SeedWork;

namespace QuokkaDev.Templates.Domain.Interfaces
{
    public interface IDomainEventsDispatcher
    {
        Task Publish(IDomainEvent domainEvent, CancellationToken cancellationToken);
        Task Notify(IDomainEventNotification<IDomainEvent> domainEventNotifications, CancellationToken cancellationToken);
    }
}
