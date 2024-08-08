
using QuokkaDev.Templates.Application.Infrastructure.Interfaces;

namespace QuokkaDev.Templates.Api.Infrastructure.HostedServices
{
    public class BatchHostedService : BackgroundService
    {
        private bool _disposedValue;
        private readonly IBatchProcessor _batchProcessor;
        private readonly ILogger<BatchHostedService> _logger;

        public BatchHostedService(IBatchProcessor batchProcessor, ILogger<BatchHostedService> logger)
        {
            _batchProcessor = batchProcessor;
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Batch Hosted Service running.");
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Batch Hosted Service is stopping.");
            return base.StopAsync(cancellationToken);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _batchProcessor.ProcessNextBatchAsync(stoppingToken);
        }
    }
}
