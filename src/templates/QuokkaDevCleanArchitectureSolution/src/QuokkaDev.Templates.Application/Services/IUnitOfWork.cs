namespace QuokkaDev.Templates.Application.Services
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Save changes to the context without dispatching domain events
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        /// <summary>
        /// Save changes to the context dispatching domain events
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);
        /// <summary>
        /// Save changes to the context dispatching domain events. The operation is wrapped in a trasaction.
        /// If errors happens the transaction is rolled back
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> TransactionalSaveEntitiesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Indicate if the current unit of work has an active transaction
        /// </summary>
        bool HasActiveTransaction { get; }
        /// <summary>
        /// Start a new transaction
        /// </summary>
        /// <returns>The started transaction</returns>
        Task<ICommandTransaction?> BeginTransactionAsync();
        /// <summary>
        /// Commit an existing transaction
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        Task CommitTransactionAsync(ICommandTransaction transaction);
        /// <summary>
        /// Rollback an existing transaction
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        Task RollbackTransactionAsync(ICommandTransaction transaction);
        /// <summary>
        /// Set an execution strategy for the unit of work
        /// </summary>
        /// <param name="actionStrategy"></param>
        /// <returns></returns>
        Task ExecuteStrategyAsync(Func<Task> actionStrategy);
        /// <summary>
        /// Reset the tracking state of the unit of work
        /// </summary>
        void Reset();
    }
}
