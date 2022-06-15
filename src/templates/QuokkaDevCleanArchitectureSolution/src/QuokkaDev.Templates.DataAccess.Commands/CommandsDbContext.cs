using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuokkaDev.Templates.Domain.Aggregates.MyAggregateRoot;
using QuokkaDev.Templates.Domain.Interfaces;
using QuokkaDev.Templates.Domain.SeedWork;

namespace QuokkaDev.Templates.DataAccess.Commands
{
    internal class CommandsDbContext : DbContext, IUnitOfWork
    {
        private readonly IDomainEventsDispatcher eventsDispatcher;

        /// <summary>
        /// Default constructor
        /// </summary>
        public CommandsDbContext()
        {
            eventsDispatcher = null!;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Configuration options</param>
        public CommandsDbContext(DbContextOptions<CommandsDbContext> context, IDomainEventsDispatcher eventsDispatcher) : base(context)
        {
            this.eventsDispatcher = eventsDispatcher;
        }

        #region Db Sets

        public DbSet<MyAggregateRoot> MyAggregateRoots => Set<MyAggregateRoot>();

        #endregion

        /// <summary>
        /// Configure EF model
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MyAggregateRoot>(ConfigureMyAggregateRoot);
        }

        /// <summary>
        /// Save entitities
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await DispatchDomainEventsAsync(cancellationToken);
            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed thought the DbContext will be commited
            var result = await base.SaveChangesAsync();

            return true;
        }

        private void ConfigureMyAggregateRoot(EntityTypeBuilder<MyAggregateRoot> myAggregateRootConfiguration)
        {
            myAggregateRootConfiguration.HasKey(a => a.Id);
        }

        private async Task DispatchDomainEventsAsync(CancellationToken cancellationToken = default)
        {
            var domainEntities = this.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents?.Any() == true);

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents!)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.DomainEvents!.Clear());

            var tasks = domainEvents
                .Select(async (domainEvent) =>
                {
                    await eventsDispatcher.Publish(domainEvent, cancellationToken);
                });

            await Task.WhenAll(tasks);
        }
    }
}
