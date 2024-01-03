using AutoMapper;
using QuokkaDev.Cqrs.Abstractions;
using QuokkaDev.Templates.Application.Services;
using QuokkaDev.Templates.Application.UseCases.Ping.Queries;
using Microsoft.Extensions.Logging;

namespace QuokkaDev.Templates.Application.UseCases.Ping.Handlers
{
    /// <summary>
    /// Handle a Ping Query request
    /// </summary>
    public class PingQueryHandler : IQueryHandler<PingQuery, PingQueryResult>
    {
        private readonly ILogger<PingQueryHandler> logger;
        private readonly IMapper mapper;
        private readonly IReportQueryService reportQueryService;

        public PingQueryHandler(ILogger<PingQueryHandler> logger, IMapper mapper, IReportQueryService reportQueryService)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.reportQueryService = reportQueryService;
        }

        public Task<PingQueryResult> Handle(PingQuery request, CancellationToken cancellation)
        {
            logger.LogTrace("handling the query...");
            // Use this line for testing DB connection: 'var dapperResult = reportQueryService.GetReportAsync(0).GetAwaiter().GetResult();'
            return Task.FromResult(new PingQueryResult($"Response: {request.EchoRequest} - {DateTime.Now:yyyyMMddHHmmssfff}"));
        }
    }
}
