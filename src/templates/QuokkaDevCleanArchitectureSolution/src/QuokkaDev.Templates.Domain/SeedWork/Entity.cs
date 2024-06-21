namespace QuokkaDev.Templates.Domain.SeedWork
{
    public abstract class Entity<KeyType> : IEntity
    {
        public virtual KeyType Id { get; init; } = default!;

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
    }
}