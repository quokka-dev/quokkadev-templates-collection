using QuokkaDev.Templates.Application.Infrastructure.Interfaces;

namespace $rootnamespace$
{
    public sealed record $safeitemname$Data() { }

    public class $safeitemname$ : IBatch<$safeitemname$Data>
    {
        private readonly IBatchCoreServices<$safeitemname$> _coreServices;

        public $safeitemname$(IBatchCoreServices<$safeitemname$> coreServices)
        {
            _coreServices = coreServices;
        }

        public async Task ProcessAsync($safeitemname$Data data)
        {

        }
    }
}
