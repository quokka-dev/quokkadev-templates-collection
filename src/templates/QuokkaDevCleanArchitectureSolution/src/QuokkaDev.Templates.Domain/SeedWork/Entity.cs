using QuokkaDev.Templates.Domain.Interfaces;

namespace QuokkaDev.Templates.Domain.SeedWork
{
    public abstract class Entity
    {
        protected List<IDomainEvent>? domainEvents;
        public List<IDomainEvent>? DomainEvents => domainEvents;

        public void AddDomainEvent(IDomainEvent eventItem)
        {
            domainEvents = domainEvents ?? new List<IDomainEvent>();
            domainEvents.Add(eventItem);
        }
        public void RemoveDomainEvent(IDomainEvent eventItem)
        {
            if (!(domainEvents is null))
            {
                domainEvents.Remove(eventItem);
            }
        }
    }
}
