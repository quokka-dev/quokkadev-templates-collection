using AutoMapper;
using QuokkaDev.AsyncNotifications.Abstractions;
using Microsoft.Extensions.Logging;

namespace $rootnamespace$
{
    public class $safeitemname$ : INotificationHandler<$fileinputname$>
    {
        private readonly ICoreServices<$safeitemname$> _coreServices;

        public $safeitemname$(ICoreServices<$safeitemname$> coreServices)
        {
            _coreServices = coreServices;
        }

        public async Task Handle($fileinputname$ notification, CancellationToken cancellation)
        {            
        }
    }
}
