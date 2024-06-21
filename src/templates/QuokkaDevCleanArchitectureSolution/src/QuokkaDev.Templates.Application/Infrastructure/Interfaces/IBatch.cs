namespace QuokkaDev.Templates.Application.Infrastructure.Interfaces
{
    public interface IBatch
    {
    }

    public interface IBatch<TBatchData> : IBatch where TBatchData : class
    {
        Task ProcessAsync(TBatchData data);
    }
}
