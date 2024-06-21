using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using QuokkaDev.Templates.Api.Infrastructure.Filters;
using QuokkaDev.Templates.Application.Infrastructure.Interfaces;
using QuokkaDev.Templates.Application.Samples.Greetings;

namespace QuokkaDev.Templates.Api.Samples
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [TypeFilter(typeof(GeneralExceptionFilter))]
    [ApiExplorerSettings(GroupName = "Samples")]
    public class HelloWorldController : ControllerBase
    {
        private readonly ICommandService _commandService;
        private readonly ILogger<HelloWorldController> logger;

        public HelloWorldController(ICommandService commandService, ILogger<HelloWorldController> logger)
        {
            _commandService = commandService;
            this.logger = logger;
        }

        [HttpGet()]
        [MapToApiVersion("1.0")]
        [ProducesResponseType<GreetingCommandResult>(200)]

        public async Task<IActionResult> Greet([FromQuery] string? name)
        {
            var greetingsCommand = new GreetingCommand(name ?? "");
            logger.LogInformation("Greet command request with name {name}", greetingsCommand.SenderMessageName);
            return Ok(await _commandService.DispatchAsync<GreetingCommand, GreetingCommandResult>(greetingsCommand));
        }
    }
}