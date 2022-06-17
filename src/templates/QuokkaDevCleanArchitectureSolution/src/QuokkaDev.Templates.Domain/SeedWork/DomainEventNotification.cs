using QuokkaDev.Templates.Domain.Interfaces;

namespace QuokkaDev.Templates.Domain.SeedWork
{
    public class DomainEventNotification<T> where T : IDomainEvent
    {
        public T DomainEvent { get; }

        public DomainEventNotification(T domainEvent)
        {
            this.DomainEvent = domainEvent;
        }
    }
}
