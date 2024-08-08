namespace QuokkaDev.Templates.Domain.SeedWork
{
    public class BaseAggregateRoot<KeyType> : Entity<KeyType>, IAggregateRoot
    {
        protected List<IDomainEvent>? domainEvents;
        public IReadOnlyCollection<IDomainEvent>? DomainEvents => domainEvents?.AsReadOnly();

        internal void AddDomainEvent(IDomainEvent eventItem)
        {
            domainEvents ??= [];
            domainEvents.Add(eventItem);
        }
        internal void RemoveDomainEvent(IDomainEvent eventItem)
        {
            domainEvents?.Remove(eventItem);
        }
        public void ClearDomainEvents()
        {
            this.domainEvents?.Clear();
        }        
    }
}
