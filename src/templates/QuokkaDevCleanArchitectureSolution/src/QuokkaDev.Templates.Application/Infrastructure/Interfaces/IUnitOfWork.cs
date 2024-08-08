namespace QuokkaDev.Templates.Application.Infrastructure.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Save changes to the context dispatching domain events
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Reset the tracking state of the unit of work
        /// </summary>
        void Reset();
    }
}
