using AutoMapper;
using Microsoft.Extensions.Logging;
using QuokkaDev.Templates.Application.Infrastructure.Interfaces;

namespace QuokkaDev.Templates.Application.Infrastructure.Services
{
    

    /// <summary>
    /// Implementation of <see cref="ICoreServices{T}"/> that provides core services required by the application.
    /// </summary>
    /// <typeparam name="T">The type of the class that requires the core services.</typeparam>    
    internal class CoreServices<T> : ICoreServices<T> where T : class
    {
        /// <summary>
        /// Gets the logger instance.
        /// </summary>
        public ILogger<T> Logger { get; }

        /// <summary>
        /// Gets the unit of work instance.
        /// </summary>
        public IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Gets the repository factory instance.
        /// </summary>
        public IRepositoryFactory RepositoryFactory { get; }

        /// <summary>
        /// Gets the mapper instance.
        /// </summary>
        public IMapper Mapper { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoreServices{T}"/> class.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="unitOfWork">The unit of work instance.</param>
        /// <param name="repositoryFactory">The repository factory instance.</param>
        /// <param name="mapper">The mapper instance.</param>
        /// <param name="notificationSender">The notification sender instance.</param>
        public CoreServices(ILogger<T> logger, IUnitOfWork unitOfWork, IRepositoryFactory repositoryFactory, IMapper mapper)
        {
            Logger = logger;
            UnitOfWork = unitOfWork;
            RepositoryFactory = repositoryFactory;
            Mapper = mapper;
        }
    }

    internal class CommandsCoreServices<T> : CoreServices<T>, ICommandsCoreServices<T> where T : class
    {
        public ICurrentUserAccessor CurrentUserAccessor { get; }

        public CommandsCoreServices(ILogger<T> logger, IUnitOfWork unitOfWork, IRepositoryFactory repositoryFactory, IMapper mapper, ICurrentUserAccessor currentUserAccessor)
            : base(logger, unitOfWork, repositoryFactory, mapper)
        {
            CurrentUserAccessor = currentUserAccessor;
        }

    }

    internal class BatchCoreServices<T> : CoreServices<T>, IBatchCoreServices<T> where T : class
    {
        public ITransactionManager TransactionManager { get; }

        public BatchCoreServices(ILogger<T> logger, IUnitOfWork unitOfWork, IRepositoryFactory repositoryFactory, IMapper mapper, ITransactionManager transactionManager)
            : base(logger, unitOfWork, repositoryFactory, mapper)
        {
            TransactionManager = transactionManager;
        }
    }
}

