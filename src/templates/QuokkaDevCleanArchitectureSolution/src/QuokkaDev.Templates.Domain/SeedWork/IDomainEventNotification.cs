using QuokkaDev.Templates.Domain.Interfaces;

namespace QuokkaDev.Templates.Domain.SeedWork
{
    /// <summary>
    /// Generic interface for handling non transactional actions  for domain events
    /// </summary>
    /// <typeparam name="TEventType"></typeparam>
    public interface IDomainEventNotification<out TEventType> where TEventType : IDomainEvent
    {
        TEventType DomainEvent { get; }
    }
}
