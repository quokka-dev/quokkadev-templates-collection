namespace QuokkaDev.Templates.Application.Infrastructure.Interfaces
{
    public interface ICommandService
    {
        Task<TCommandResult> DispatchAsync<TCommand, TCommandResult>(TCommand command, CancellationToken cancellation);

        Task<TCommandResult> DispatchAsync<TCommand, TCommandResult>(TCommand command);
    }
}
