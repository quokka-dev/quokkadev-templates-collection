using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using QuokkaDev.Templates.Application.Infrastructure.Interfaces;
using QuokkaDev.Templates.Domain.Samples.HelloWorldMessages;
using QuokkaDev.Templates.Persistence.Ef.Infrastructure.Utils;
using System.Data;

namespace QuokkaDev.Templates.Persistence.Ef.Infrastructure.Implementations
{
    internal class ApplicationDbContext : DbContext, IUnitOfWork, ITransactionManager, IDataAccessBootstrapper
    {
        private readonly ILogger<ApplicationDbContext> logger;
        private IDbContextTransaction? currentTransaction = null;

        /// <summary>
        /// Default constructor
        /// </summary>
        public ApplicationDbContext()
        {
            logger = null!;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Configuration options</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> context, ILogger<ApplicationDbContext> logger) : base(context)
        {
            this.logger = logger;
        }

        #region Db Sets

        // Put your DbSet here
        internal DbSet<AuditEntry> AuditEntries => Set<AuditEntry>();
        internal DbSet<HelloWorldMessage> HelloWorldMessages => Set<HelloWorldMessage>();

        #endregion

        public bool HasActiveTransaction => currentTransaction != null;

        /// <summary>
        /// Configure EF model
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
            configurationBuilder.AutoRegisterConverters();
        }

        /// <summary>
        /// Save entitities
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await SaveChangesAsync(cancellationToken);
            return true;
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

        public async Task ExecuteInTransactionAsync(Func<Task> actionStrategy)
        {
            var strategy = Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () => await actionStrategy());
        }

        public void Reset()
        {
            ChangeTracker.Clear();
        }

        public async Task BootstrapAsync()
        {
            //await this.Database.EnsureDeletedAsync();
            if ((await this.Database.GetPendingMigrationsAsync()).Any())
            {
                await this.Database.MigrateAsync();
            }
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
