using QuokkaDev.Templates.Domain.Interfaces;
using QuokkaDev.Templates.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuokkaDev.Templates.DataAccess.Commands.Repositories
{
    internal abstract class BaseRepository<T> : IRepository<T>
        where T : class, IAggregateRoot

    {
        protected readonly CommandsDbContext context;
        protected readonly DbSet<T> set;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return context;
            }
        }

        public BaseRepository(CommandsDbContext context)
        {
            this.context = context;
            this.set = context.Set<T>();
        }

        public async Task<T> CreateAsync(T entity)
        {
            return (await this.set.AddAsync(entity)).Entity;
        }

        public Task DeleteAsync(T entity)
        {
            this.set.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(object key)
        {
            var entity = await this.set.FindAsync(key);
            if (entity != null)
            {
                await DeleteAsync(entity);
            }
        }

        public async Task<T?> FindByKeyAsync(object key)
        {
            return await this.set.FindAsync(key);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await this.set.ToListAsync();
        }

        public void Update(T entity)
        {
            this.set.Update(entity);
        }
    }
}
