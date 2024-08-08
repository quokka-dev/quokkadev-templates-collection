using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuokkaDev.Templates.Application.Infrastructure.Interfaces
{
    /// <summary>
    /// Provides core services required by the application.
    /// </summary>
    /// <typeparam name="T">The type of the class that requires the core services.</typeparam>
    public interface ICoreServices<T> where T : class
    {
        /// <summary>
        /// Gets the logger instance.
        /// </summary>
        ILogger<T> Logger { get; }

        /// <summary>
        /// Gets the mapper instance.
        /// </summary>
        IMapper Mapper { get; }

        /// <summary>
        /// Gets the repository factory instance.
        /// </summary>
        IRepositoryFactory RepositoryFactory { get; }

        /// <summary>
        /// Gets the unit of work instance.
        /// </summary>
        IUnitOfWork UnitOfWork { get; }
    }

    /// <summary>
    /// Provides core services required by command handlers.
    /// </summary>
    /// <typeparam name="T">The type of the class that requires the core services.</typeparam>
    public interface ICommandsCoreServices<T> : ICoreServices<T> where T : class
    {
        /// <summary>
        /// Gets the current user accessor instance.
        /// </summary>
        ICurrentUserAccessor CurrentUserAccessor { get; }
    }

    /// <summary>
    /// Provides core services required by batch operations.
    /// </summary>
    /// <typeparam name="T">The type of the class that requires the core services.</typeparam>
    public interface IBatchCoreServices<T> : ICoreServices<T> where T : class
    {
        /// <summary>
        /// Gets the transaction manager instance.
        /// </summary>
        ITransactionManager TransactionManager { get; }
    }
}
