using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuokkaDev.Templates.Application.Infrastructure.Interfaces
{
    public interface ITransactionManager
    {
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
        Task ExecuteInTransactionAsync(Func<Task> actionStrategy);
    }
}
