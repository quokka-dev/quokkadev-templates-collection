using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using QuokkaDev.Cqrs.Abstractions;
using QuokkaDev.Templates.Api.Filters;
using QuokkaDev.Templates.Application.UseCases.Ping.Queries;

namespace QuokkaDev.Templates.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [TypeFilter(typeof(GeneralExceptionFilter))]
    public class PingController : ControllerBase
    {
        private readonly ICommandDispatcher commandDispatcher;
        private readonly IQueryDispatcher queryDispatcher;
        private readonly ILogger<PingController> logger;

        public PingController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher, ILogger<PingController> logger)
        {
            this.commandDispatcher = commandDispatcher;
            this.queryDispatcher = queryDispatcher;
            this.logger = logger;
        }

        [HttpGet()]
        public async Task<IActionResult> Ping([FromQuery] string? echoRequest)
        {
            var pingRequest = new PingQuery(echoRequest ?? "");
            logger.LogInformation("Pinq request");
            return Ok(await queryDispatcher.Dispatch<PingQuery, PingQueryResult>(pingRequest));
        }
    }
}