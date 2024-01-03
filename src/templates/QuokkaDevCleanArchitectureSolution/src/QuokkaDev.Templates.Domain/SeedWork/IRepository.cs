namespace QuokkaDev.Templates.Domain.SeedWork
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        Task<T?> FindByKeyAsync(object key);
        Task<IReadOnlyCollection<T>> FindAllAsync(Specification<T> specification);
        Task<T?> FindFirstAsync(Specification<T> specification);
        T Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
