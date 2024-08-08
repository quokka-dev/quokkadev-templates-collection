using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using QuokkaDev.Templates.Application.Infrastructure.Interfaces;

namespace QuokkaDev.Templates.Persistence.Ef.Infrastructure.Implementations
{
    internal class SpannedTransaction : ISpannedTransaction
    {
        private readonly DbContext[] _dbContexts;

        internal SpannedTransaction(params DbContext[] dbContexts)
        {
            if (dbContexts == null || !dbContexts.Any())
            {
                throw new ArgumentException("At least one DbContext should be provided");
            }
            _dbContexts = dbContexts;
        }

        public async Task ExecuteAsync(Func<Task> action)
        {            
            var firstContext = _dbContexts.First();
            var strategy = firstContext.Database.CreateExecutionStrategy();
            
            await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await firstContext.Database.BeginTransactionAsync();
                var connection = firstContext.Database.GetDbConnection();

                // Set the same connection and transaction for all others contexts
                foreach (var dbContext in _dbContexts.Skip(1))
                {
                    dbContext.Database.SetDbConnection(connection);
                    dbContext.Database.UseTransaction(transaction.GetDbTransaction());
                }
               
                await action();
                await transaction.CommitAsync();
            });
        }        
    }

    public class SpannedTransactionBuilder : ISpannedTransactionBuilder
    {
        private readonly List<DbContext> _dbContexts = new List<DbContext>();

        internal SpannedTransactionBuilder(object dbContext)
        {
            TryContext(dbContext);
        }

        public ISpannedTransactionBuilder SpanTo(object dbContext)
        {
            TryContext(dbContext);
            return this;
        }

        public ISpannedTransaction Build()
        {
            return new SpannedTransaction(_dbContexts.ToArray());
        }

        private void TryContext(object dbContext)
        {
            if (dbContext is DbContext context)
            {
                _dbContexts.Add(context);
            }
            else
            {
                throw new ArgumentException("Invalid DbContext provided");
            }
        }
    }

    public class SpannedTransactionBuilderFactory : ISpannedTransactionBuilderFactory
    {
        public ISpannedTransactionBuilder SpannedTransactionFrom(object dbContext)
        {
            return new SpannedTransactionBuilder(dbContext);
        }
    }
}
