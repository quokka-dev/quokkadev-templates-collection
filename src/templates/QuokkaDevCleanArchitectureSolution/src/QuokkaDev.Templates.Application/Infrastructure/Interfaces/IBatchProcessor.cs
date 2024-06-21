namespace QuokkaDev.Templates.Application.Infrastructure.Interfaces
{
    public interface IBatchProcessor
    {
        Task ProcessNextBatchAsync(CancellationToken stoppingToken);
    }
}