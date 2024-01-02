using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using QuokkaDev.Templates.Domain.Interfaces;
using QuokkaDev.Templates.Domain.MyAggregateRoot;
using QuokkaDev.Templates.Domain.SeedWork;

namespace QuokkaDev.Templates.DataAccess.Commands
{
    internal class CommandsDbContext : DbContext, IUnitOfWork
    {
        private readonly IDomainEventsDispatcher eventsDispatcher;
        private readonly ILogger<CommandsDbContext> logger;
        private readonly List<IDomainEvent> allEvents = new List<IDomainEvent>();

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
        public CommandsDbContext(DbContextOptions<CommandsDbContext> context, IDomainEventsDispatcher eventsDispatcher, ILogger<CommandsDbContext> logger) : base(context)
        {
            this.eventsDispatcher = eventsDispatcher;
            this.logger = logger;
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
            var domainEvents = GetDomainEvents();
            this.allEvents.AddRange(domainEvents);
            if (this.Database.CurrentTransaction == null)
            {
                using IDbContextTransaction transaction = await this.Database.BeginTransactionAsync();
                try
                {
                    await base.SaveChangesAsync();
                    await DispatchDomainEventsAsync(domainEvents, cancellationToken);
                    await transaction.CommitAsync();
                    await NotifyDomainEventsAsync(this.allEvents, cancellationToken);
                    this.allEvents.Clear();
                    return true;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    logger?.LogError("Rolling back changes occured while saving DB changes, {error}", ex.Message);
                    throw;
                }
            }
            else
            {
                await base.SaveChangesAsync();
                await DispatchDomainEventsAsync(domainEvents, cancellationToken);
                return true;
            }

        }

        private void ConfigureMyAggregateRoot(EntityTypeBuilder<MyAggregateRoot> myAggregateRootConfiguration)
        {
            myAggregateRootConfiguration.HasKey(a => a.Id);
        }

        private IEnumerable<IDomainEvent> GetDomainEvents()
        {
            var domainEntities = this.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents?.Any() == true);

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents!)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.DomainEvents!.Clear());

            return domainEvents;
        }

        private async Task DispatchDomainEventsAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default)
        {
            var tasks = domainEvents
                .Select(async (domainEvent) =>
                {
                    await eventsDispatcher.Publish(domainEvent, cancellationToken);
                });

            await Task.WhenAll(tasks);
        }

        private async Task NotifyDomainEventsAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default)
        {
            var tasks = domainEvents
                .Select(async (domainEvent) =>
                {
                    await eventsDispatcher.Notify(domainEvent, cancellationToken);
                });

            await Task.WhenAll(tasks);
        }
    }
}
