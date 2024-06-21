namespace QuokkaDev.Templates.Application.Infrastructure.Interfaces
{
    public interface IBatchQueueService
    {
        Guid Enqueue<TBatchType, TBatchData>(TBatchData data)
            where TBatchType : IBatch<TBatchData>
            where TBatchData : class;
    }
}