using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuokkaDev.Templates.Application.Infrastructure.Interfaces
{
    /// <summary>
    /// Represents a transaction that can span multiple database contexts.
    /// </summary>
    public interface ISpannedTransaction
    {
        /// <summary>
        /// Executes the specified action within the transaction.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task ExecuteAsync(Func<Task> action);
    }

    /// <summary>
    /// Builder interface for creating spanned transactions.
    /// </summary>
    public interface ISpannedTransactionBuilder
    {
        /// <summary>
        /// Adds a database context to the spanned transaction.
        /// </summary>
        /// <param name="dbContext">The database context to add.</param>
        /// <returns>The current instance of the builder.</returns>
        ISpannedTransactionBuilder SpanTo(object dbContext);
    }

    /// <summary>
    /// Factory interface for creating spanned transaction builders.
    /// </summary>
    public interface ISpannedTransactionBuilderFactory
    {
        /// <summary>
        /// Creates a spanned transaction builder from the specified database context.
        /// </summary>
        /// <param name="dbContext">The database context to start the transaction from.</param>
        /// <returns>A new instance of a spanned transaction builder.</returns>
        ISpannedTransactionBuilder SpannedTransactionFrom(object dbContext);
    }
}
