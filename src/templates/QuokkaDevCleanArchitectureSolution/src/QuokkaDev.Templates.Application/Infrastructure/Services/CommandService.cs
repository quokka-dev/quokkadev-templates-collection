using QuokkaDev.Cqrs.Abstractions;
using QuokkaDev.Templates.Application.Infrastructure.Interfaces;

namespace QuokkaDev.Templates.Application.Infrastructure.Services
{
    internal class CommandService : ICommandService
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public CommandService(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        public Task<TCommandResult> DispatchAsync<TCommand, TCommandResult>(TCommand command, CancellationToken cancellation)
        {
            return _commandDispatcher.Dispatch<TCommand, TCommandResult>(command, cancellation);
        }

        public Task<TCommandResult> DispatchAsync<TCommand, TCommandResult>(TCommand command)
        {
            return _commandDispatcher.Dispatch<TCommand, TCommandResult>(command);
        }
    }
}
