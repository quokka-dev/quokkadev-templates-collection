namespace $rootnamespace$
{
    public sealed record $safeitemname$Data() { }

    public class $safeitemname$ : IBatch<$safeitemname$Data>
    {
        private readonly ICoreServices<$safeitemname$> _coreServices;

        public $safeitemname$(ICoreServices<$safeitemname$> coreServices)
        {
            _coreServices = coreServices;
        }

        public async Task ProcessAsync($safeitemname$Data data)
        {

        }
    }
}
