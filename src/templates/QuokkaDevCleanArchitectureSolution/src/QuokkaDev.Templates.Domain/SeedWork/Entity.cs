namespace QuokkaDev.Templates.Domain.SeedWork
{
    public abstract class Entity<KeyType> : IEntity
    {
        public required virtual KeyType Id { get; init; } = default!;

        /// <summary>
        /// Override Equals method to check entity Id
        /// </summary>
        /// <param name="obj">object to compare</param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return Id!.Equals(((Entity<KeyType>)obj).Id);
        }

        /// <summary>
        /// Override GetHashCode for comparing entity Id
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Id!.GetHashCode();
        }

        protected void AddKeyToCollection<TKey>(TKey key, ICollection<TKey> collection)
        {
            if (collection.Any(s => s!.Equals(key)))
            {
                throw new InvalidOperationException($"{nameof(TKey)} {key} is already present");
            }
            collection.Add(key);
        }

        protected void RemoveKeyFromCollection<TKey>(TKey key, ICollection<TKey> collection)
        {
            if (!collection.Any(s => s!.Equals(key)))
            {
                throw new InvalidOperationException($"{nameof(TKey)} {key} is not associated to {GetType().Name} {Id}");
            }
            collection.Remove(key);
        }
    }
}