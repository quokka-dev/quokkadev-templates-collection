namespace QuokkaDev.Templates.Domain.SeedWork
{
    public interface IAggregateRoot
    {
        IReadOnlyCollection<IDomainEvent>? DomainEvents { get; }        
        void ClearDomainEvents();
    }
}
