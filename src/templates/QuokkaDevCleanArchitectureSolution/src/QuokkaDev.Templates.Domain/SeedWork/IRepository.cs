namespace QuokkaDev.Templates.Domain.SeedWork
{
    public interface IRepository
    {
    }

    public interface IRepository<T> : IRepository where T : IAggregateRoot
    {
        Task<T?> FindByKeyAsync(object key);
        Task<IReadOnlyCollection<T>> FindAllAsync(Specification<T> specification);
        Task<T?> FindFirstAsync(Specification<T> specification);
        T Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task LoadPropertyAsync<TProperty>(T entity, System.Linq.Expressions.Expression<Func<T, TProperty?>> property) where TProperty : class, IEntity;
        Task LoadPropertiesAsync<TProperty>(T entity, System.Linq.Expressions.Expression<Func<T, IEnumerable<TProperty>>> collection) where TProperty : class, IEntity;
    }
}
