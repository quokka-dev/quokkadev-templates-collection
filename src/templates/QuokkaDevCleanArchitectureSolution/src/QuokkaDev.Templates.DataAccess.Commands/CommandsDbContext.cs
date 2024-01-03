using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using QuokkaDev.Templates.Application.Services;
using QuokkaDev.Templates.Domain.SeedWork;
using System.Data;

namespace QuokkaDev.Templates.DataAccess.Commands
{
    internal class CommandsDbContext : DbContext, IUnitOfWork
    {
        private readonly IDomainEventsDispatcher eventsDispatcher;
        private readonly ILogger<CommandsDbContext> logger;
        private IDbContextTransaction? currentTransaction = null;

        /// <summary>
        /// Default constructor
        /// </summary>
        public CommandsDbContext()
        {
            eventsDispatcher = null!;
            logger = null!;
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

        // Put your DbSet here

        #endregion

        public bool HasActiveTransaction => currentTransaction != null;

        /// <summary>
        /// Configure EF model
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CommandsDbContext).Assembly);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
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
            await this.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> TransactionalSaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            bool result;
            var transaction = await this.BeginTransactionAsync();

            try
            {
                result = await this.SaveEntitiesAsync(cancellationToken);
                await this.CommitTransactionAsync(transaction!);
                return result;
            }
            catch (Exception)
            {
                await this.RollbackTransactionAsync(transaction!);
                throw;
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        public async Task<ICommandTransaction?> BeginTransactionAsync()
        {
            if (currentTransaction is not null)
            {
                return null;
            }

            currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
            return new CommandTransaction(currentTransaction);
        }

        public Task CommitTransactionAsync(ICommandTransaction transaction)
        {
            CheckTransaction(transaction);
            return CommitTransactionInternalAsync(transaction);
        }

        public Task RollbackTransactionAsync(ICommandTransaction transaction)
        {
            CheckTransaction(transaction);
            return RollbackTransactionInternalAsync(transaction);
        }

        public async Task ExecuteStrategyAsync(Func<Task> actionStrategy)
        {
            var strategy = Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () => await actionStrategy());
        }

        public void Reset()
        {
            this.ChangeTracker.Clear();
        }

        private async Task DispatchDomainEventsAsync(CancellationToken cancellationToken = default)
        {
            var domainEntities = this.ChangeTracker
                .Entries<IAggregateRoot>()
                .Where(x => x.Entity.DomainEvents?.Any() == true);

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents!)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            var tasks = domainEvents
                .Select(async (domainEvent) => await eventsDispatcher.Publish(domainEvent, cancellationToken));

            await Task.WhenAll(tasks);
        }

        private async Task CommitTransactionInternalAsync(ICommandTransaction transaction)
        {
            try
            {
                await transaction.CommitAsync();
            }
            finally
            {
                if (currentTransaction != null)
                {
                    currentTransaction.Dispose();
                    currentTransaction = null;
                }
            }
        }

        private async Task RollbackTransactionInternalAsync(ICommandTransaction transaction)
        {
            try
            {
                await transaction!.RollbackAsync();
            }
            finally
            {
                if (currentTransaction != null)
                {
                    currentTransaction.Dispose();
                    currentTransaction = null;
                }
            }
        }

        private void CheckTransaction(ICommandTransaction transaction)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            if (transaction.Id != currentTransaction?.TransactionId)
            {
                throw new InvalidOperationException($"Transaction {transaction.Id} is not current");
            }
        }
    }
}
