using QuokkaDev.Templates.Application.Infrastructure.Interfaces;
using QuokkaDev.Templates.Application.Infrastructure.Services;

namespace QuokkaDev.Templates.Application.Samples.Batches
{
    public sealed record SampleLongRunningBatchData(string Message = "Working...") { }

    public class SampleLongRunningBatch : IBatch<SampleLongRunningBatchData>
    {
        private readonly ICoreServices<SampleLongRunningBatch> _coreServices;

        public SampleLongRunningBatch(ICoreServices<SampleLongRunningBatch> coreServices)
        {
            _coreServices = coreServices;
        }

        public async Task ProcessAsync(SampleLongRunningBatchData data)
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"{data.Message} {i}");
                await Task.Delay(1000);
            }
        }
    }
}
