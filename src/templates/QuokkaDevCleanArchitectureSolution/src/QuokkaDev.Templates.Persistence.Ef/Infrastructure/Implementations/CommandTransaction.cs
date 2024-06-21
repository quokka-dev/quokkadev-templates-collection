using Microsoft.EntityFrameworkCore.Storage;
using QuokkaDev.Templates.Application.Infrastructure.Interfaces;

namespace QuokkaDev.Templates.Persistence.Ef.Infrastructure.Implementations
{
    internal sealed class CommandTransaction : ICommandTransaction
    {
        private readonly IDbContextTransaction dbContextTransaction;
        private bool _disposed = false;
        public Guid Id => dbContextTransaction.TransactionId;

        public CommandTransaction(IDbContextTransaction dbContextTransaction)
        {
            this.dbContextTransaction = dbContextTransaction;
        }

        public void Commit()
        {
            dbContextTransaction.Commit();
        }

        public Task CommitAsync()
        {
            return dbContextTransaction.CommitAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Rollback()
        {
            dbContextTransaction.Rollback();
        }

        public Task RollbackAsync()
        {
            return dbContextTransaction.RollbackAsync();
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                dbContextTransaction.Dispose();
            }

            _disposed = true;
        }
    }
}
