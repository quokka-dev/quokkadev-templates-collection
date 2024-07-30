using Microsoft.Extensions.Logging;
using QuokkaDev.Cqrs.Abstractions;
using QuokkaDev.Templates.Application.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuokkaDev.Templates.Application.Infrastructure.Services
{
    internal class CommandTransactionDecorator : ICommandDispatcher
    {
        private readonly ICommandDispatcher _dispatcher;
        private readonly ITransactionManager _transactionManager;
        private readonly ILogger<CommandTransactionDecorator> _logger;

        public CommandTransactionDecorator(ICommandDispatcher dispatcher, ITransactionManager transactionManager, ILogger<CommandTransactionDecorator> logger)
        {
            this._dispatcher = dispatcher;
            this._transactionManager = transactionManager;
            this._logger = logger;
        }
        public async Task<TCommandResult> Dispatch<TCommand, TCommandResult>(TCommand command, CancellationToken cancellation)
        {
            var result = default(TCommandResult);

            if (_transactionManager.HasActiveTransaction)
            {
                return await _dispatcher.Dispatch<TCommand, TCommandResult>(command, cancellation);
            }

            await _transactionManager.ExecuteInTransactionAsync(async () =>
            {
                using var transaction = await _transactionManager.BeginTransactionAsync();
                if (transaction is not null)
                {
                    using (_logger.BeginScope(new Dictionary<string, string>(){ { "TransactionContext", transaction.Id.ToString() } }))
                    {
                        try
                        {
                            var typeName = typeof(TCommand).Name;

                            _logger.LogInformation("----- Begin transaction {TransactionId} for {CommandName} ({@Command})", transaction.Id, typeName, command);
                            result = await _dispatcher.Dispatch<TCommand, TCommandResult>(command, cancellation).ConfigureAwait(true);
                            _logger.LogInformation("----- Commit transaction {TransactionId} for {CommandName}", transaction.Id, typeName);

                            await _transactionManager.CommitTransactionAsync(transaction).ConfigureAwait(true);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "{Message}", ex.Message);
                            await _transactionManager.RollbackTransactionAsync(transaction);
                            throw;
                        }
                    }
                }
                else
                {
                    throw new InvalidOperationException("Cannot start a new transaction");
                }
            }).ConfigureAwait(true);

            return result!;
        }

        public Task<TCommandResult> Dispatch<TCommand, TCommandResult>(TCommand command)
        {
            return Dispatch<TCommand, TCommandResult>(command, CancellationToken.None);
        }
    }
}
