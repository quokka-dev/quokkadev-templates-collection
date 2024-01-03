namespace QuokkaDev.Templates.Domain.SeedWork
{
    public class BaseAggregateRoot<KeyType> : Entity<KeyType>, IAggregateRoot
    {
        protected List<IDomainEvent>? domainEvents;
        public IReadOnlyCollection<IDomainEvent>? DomainEvents => domainEvents?.AsReadOnly();

        public void AddDomainEvent(IDomainEvent eventItem)
        {
            domainEvents ??= [];
            domainEvents.Add(eventItem);
        }
        public void RemoveDomainEvent(IDomainEvent eventItem)
        {
            domainEvents?.Remove(eventItem);
        }
        public void ClearDomainEvents()
        {
            this.domainEvents?.Clear();
        }

        protected void AddEntityToCollection<TEntity, TKey>(TEntity entity, ICollection<TEntity> collection) where TEntity : Entity<TKey>
        {
            if (collection.Any(s => s.Id!.Equals(entity.Id)))
            {
                throw new InvalidOperationException($"{nameof(TEntity)} {entity.Id} is already present");
            }

            collection.Add(entity);
        }

        protected void RemoveEntityFromCollection<TEntity, TKey>(TEntity entity, ICollection<TEntity> collection) where TEntity : Entity<TKey>
        {
            if (!collection.Any(s => s.Id!.Equals(entity.Id)))
            {
                throw new InvalidOperationException($"{nameof(TEntity)} {entity.Id} is not associated to {this.GetType().Name} {Id}");
            }
            collection.Remove(entity);
        }

        protected void CheckEntityReferenceToAggregate(Func<KeyType> referenceID)
        {
            object? keyDefaultValue = Id is IDefaultValue dv ? dv.GetDefaultValue() : Id;

            if (!(referenceID()!.Equals(this.Id)) &&
                !(referenceID()!.Equals(keyDefaultValue)))
            {
                throw new ArgumentException($"Child entity has a reference to another {this.GetType().Name} aggregate {referenceID}");
            }
        }
    }
}
