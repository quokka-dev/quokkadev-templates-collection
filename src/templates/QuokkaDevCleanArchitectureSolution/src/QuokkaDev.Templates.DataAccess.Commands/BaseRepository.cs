using Microsoft.EntityFrameworkCore;
using QuokkaDev.Templates.Domain.SeedWork;

namespace QuokkaDev.Templates.DataAccess.Commands
{
    internal abstract class BaseRepository<T> : IRepository<T>
        where T : class, IAggregateRoot

    {
        protected readonly CommandsDbContext context;
        protected readonly DbSet<T> set;


        protected BaseRepository(CommandsDbContext context)
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
    }
}
