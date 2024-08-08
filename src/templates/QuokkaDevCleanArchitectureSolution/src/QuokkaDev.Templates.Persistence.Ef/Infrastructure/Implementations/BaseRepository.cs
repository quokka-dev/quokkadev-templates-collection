using Microsoft.EntityFrameworkCore;
using QuokkaDev.Templates.Domain.SeedWork;
using System.Linq.Expressions;

namespace QuokkaDev.Templates.Persistence.Ef.Infrastructure.Implementations
{
    internal abstract class BaseRepository<T> : IRepository<T>
        where T : class, IAggregateRoot

    {
        protected readonly DbContext context;
        protected readonly DbSet<T> set;


        protected BaseRepository(DbContext context)
        {
            this.context = context;
            set = context.Set<T>();
        }

        public virtual T Create(T entity)
        {
            return set.Add(entity).Entity;
        }

        public virtual void Delete(T entity)
        {
            set.Remove(entity);
        }

        public virtual async Task<IReadOnlyCollection<T>> FindAllAsync(Specification<T> specification)
        {
            return await set.AsNoTracking().Where(specification.ToExpression()).ToListAsync();
        }

        public virtual async Task<T?> FindFirstAsync(Specification<T> specification)
        {
            return await set.AsNoTracking().FirstOrDefaultAsync(specification.ToExpression());
        }

        public virtual async Task<T?> FindByKeyAsync(object key)
        {
            T? entity = await set.FindAsync(key);

            if (entity != null)
            {
                context.Entry(entity).State = EntityState.Detached;
            }

            return entity;
        }

        public virtual void Update(T entity)
        {
            set.Update(entity);
        }

        public virtual async Task LoadPropertyAsync<TProperty>(T entity, Expression<Func<T, TProperty?>> property) where TProperty : class, IEntity
        {
            var entry = set.Entry(entity);
            if (entry != null)
            {
                if (entry.State == EntityState.Detached)
                {
                    set.Attach(entity);
                }
                await entry.Reference(property).LoadAsync();
            }
        }

        public virtual async Task LoadPropertiesAsync<TProperty>(T entity, Expression<Func<T, IEnumerable<TProperty>>> collection) where TProperty : class, IEntity
        {
            var entry = set.Entry(entity);
            if (entry != null)
            {
                if (entry.State == EntityState.Detached)
                {
                    set.Attach(entity);
                }
                await entry.Collection(collection).LoadAsync();
            }
        }
    }
}
