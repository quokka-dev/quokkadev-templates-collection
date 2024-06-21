using Microsoft.Extensions.Logging;
using QuokkaDev.Cqrs.Abstractions;
using QuokkaDev.Templates.Application.Infrastructure.Services;
using QuokkaDev.Templates.Domain.Samples.HelloWorldMessages;

namespace QuokkaDev.Templates.Application.Samples.Greetings
{
    /// <summary>
    /// Represents a command to generate a greeting message.
    /// </summary>
    public sealed record GreetingCommand(string SenderMessageName)
    {
    }

    /// <summary>
    /// Represents the result of a GreetingCommand.
    /// </summary>
    public sealed record GreetingCommandResult(string GreetingMessage)
    {
    }

    /// <summary>
    /// Handles the processing of GreetingCommand to generate a GreetingCommandResult.
    /// This handler is responsible for creating a greeting message based on the sender's name provided in the command.
    /// </summary>
    public class GreetingCommandHandler : ICommandHandler<GreetingCommand, GreetingCommandResult>
    {
        private readonly ICoreServices<GreetingCommandHandler> _coreServices;

        /// <summary>
        /// Initializes a new instance of the <see cref="GreetingCommandHandler"/> class.
        /// </summary>
        /// <param name="logger">The logger to be used by the handler.</param>
        /// <param name="unitOfWork">The unit of work for managing database transactions.</param>
        /// <param name="repositoryFactory">The factory for creating repositories.</param>
        public GreetingCommandHandler(ICoreServices<GreetingCommandHandler> coreServices)
        {
            _coreServices = coreServices;
        }

        /// <summary>
        /// Handles the given GreetingCommand and returns a GreetingCommandResult.
        /// </summary>
        /// <param name="request">The GreetingCommand to handle.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation, containing the GreetingCommandResult.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the request is null.</exception>
        public async Task<GreetingCommandResult> Handle(GreetingCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            _coreServices.Logger.LogInformation($"Processing greeting for {request.SenderMessageName}");

            var helloWorldMessageRepository = _coreServices.RepositoryFactory.GetRepository<IHelloWorldMessageRepository>();

            HelloWorldMessage message = HelloWorldMessageFactory.CreateNewHelloWorldMessage($"Hi {request.SenderMessageName}");

            helloWorldMessageRepository.Create(message);
            await _coreServices.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            _coreServices.Logger.LogInformation($"Greeting processed for {request.SenderMessageName}");

            return new GreetingCommandResult(message.Message);
        }
    }
}
