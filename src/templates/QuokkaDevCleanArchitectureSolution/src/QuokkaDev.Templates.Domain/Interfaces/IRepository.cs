using QuokkaDev.Templates.Domain.SeedWork;

namespace QuokkaDev.Templates.Domain.Interfaces
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
        Task<T?> FindByKeyAsync(object key);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> CreateAsync(T entity);
        void Update(T entity);
        Task DeleteAsync(T entity);
        Task DeleteAsync(object key);
    }
}
