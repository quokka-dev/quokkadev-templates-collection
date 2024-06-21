using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QuokkaDev.Templates.Application.Infrastructure.Interfaces;
using System.Collections.Concurrent;

namespace QuokkaDev.Templates.Application.Infrastructure.Services
{
    internal class BatchService : IBatchQueueService, IBatchProcessor
    {
        private readonly ConcurrentQueue<(Type BatchType, object BatchData, Guid BatchId)> _batches = new();
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<BatchService> _logger;

        public BatchService(IServiceProvider serviceProvider, ILogger<BatchService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public Guid Enqueue<TBatchType, TBatchData>(TBatchData data)
            where TBatchType : IBatch<TBatchData>
            where TBatchData : class
        {
            Guid batchId = Guid.NewGuid();
            _batches.Enqueue((typeof(TBatchType), data, batchId));

            return batchId;
        }

        public async Task ProcessNextBatchAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Checking for next batch to process...");
                if (_batches.TryDequeue(out var batchInfo))
                {
                    _logger.LogInformation("Processing batch {id}", batchInfo.BatchId);

                    var batchType = batchInfo.BatchType;
                    var batchData = batchInfo.BatchData;
                    var id = batchInfo.BatchId;

                    using var scope = _serviceProvider.CreateAsyncScope();

                    try
                    {
                        object batch = scope.ServiceProvider.GetRequiredService(batchType);

                        var processMethod = batchType.GetMethod("ProcessAsync");
                        var task = processMethod?.Invoke(batch, new object[] { batchData }) as Task ?? Task.CompletedTask;
                        await task;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error processing batch {id} - {message}", id, ex.Message);
                    }

                }
                else
                {
                    _logger.LogInformation("No batch to process, waiting...");
                    await Task.Delay(5000, stoppingToken);
                }
            }

            _logger.LogInformation("Batch processing stopped");
        }
    }
}
