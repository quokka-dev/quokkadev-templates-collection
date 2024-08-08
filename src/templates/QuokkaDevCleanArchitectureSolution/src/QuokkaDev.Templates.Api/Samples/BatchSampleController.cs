using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using QuokkaDev.Templates.Api.Infrastructure.Filters;
using QuokkaDev.Templates.Application.Infrastructure.Interfaces;
using QuokkaDev.Templates.Application.Samples.Batches;

namespace QuokkaDev.Templates.Api.Samples
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [TypeFilter(typeof(GeneralExceptionFilter))]
    [ApiExplorerSettings(GroupName = "Samples")]
    public class BatchSampleController : ControllerBase
    {
        private readonly IBatchQueueService _batchQueueService;

        public BatchSampleController(IBatchQueueService batchQueueService)
        {
            _batchQueueService = batchQueueService;
        }

        [HttpPost("enqueue")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult EnqueueBatch([FromBody] SampleLongRunningBatchData sampleLongRunningBatchData)
        {
            var batchId = _batchQueueService.Enqueue<SampleLongRunningBatch, SampleLongRunningBatchData>(sampleLongRunningBatchData);
            return Ok(new { BatchId = batchId });
        }
    }
}
