namespace QuokkaDev.Templates.Application.Services
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
