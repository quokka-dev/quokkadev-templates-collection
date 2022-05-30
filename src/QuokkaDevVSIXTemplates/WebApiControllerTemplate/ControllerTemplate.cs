using QuokkaDev.Cqrs.Abstractions;
using QuokkaDev.Templates.Application.UseCases.Ping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace $rootnamespace$
{
    /// <summary>
    /// $fileinputname$ Controller    
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class $fileinputname$ : ControllerBase
    {
        private readonly ICommandDispatcher commandDispatcher;
        private readonly IQueryDispatcher queryDispatcher;
        private readonly ILogger<$fileinputname$> logger;

        public $fileinputname$(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher, ILogger<$fileinputname$> logger)
        {
            this.commandDispatcher = commandDispatcher;
            this.queryDispatcher = queryDispatcher;
            this.logger = logger;
        }

        /// <summary>
        /// My Get method
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet("{id}")]
        [Authorize(Policy = "MyPolicy"]
        public async Task<IActionResult> Get(string id)
        {            
            // var result = await queryDispatcher.Dispatch(new Query());
            return Ok();
        }

        /// <summary>
        /// My Post method
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpPost()]
        [Authorize(Policy = "MyPolicy"]
        public async Task<IActionResult> Post([FromBody]object payload)
        {
            // var result = await commandDispatcher.Dispatch(new Command());
            return Ok();
        }
    }
}