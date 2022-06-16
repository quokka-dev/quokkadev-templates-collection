using QuokkaDev.Templates.Domain.Interfaces;

namespace QuokkaDev.Templates.Domain.SeedWork
{
    public class DomainNotificationBase<T> : IDomainEventNotification<T> where T : IDomainEvent
    {
        public T DomainEvent { get; }

        public DomainNotificationBase(T domainEvent)
        {
            this.DomainEvent = domainEvent;
        }
    }
}
