namespace QuokkaDev.Templates.Application.Infrastructure.Interfaces
{
    public interface ICommandTransaction : IDisposable
    {
        Guid Id { get; }

        void Commit();
        void Rollback();

        Task CommitAsync();
        Task RollbackAsync();
    }
}
