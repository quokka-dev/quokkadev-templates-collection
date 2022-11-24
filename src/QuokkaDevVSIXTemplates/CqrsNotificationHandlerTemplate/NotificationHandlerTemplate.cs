using AutoMapper;
using QuokkaDev.AsyncNotifications.Abstractions;
using Microsoft.Extensions.Logging;

namespace $rootnamespace$
{
    public class $safeitemname$ : INotificationHandler<$fileinputname$>
    {
        private readonly ILogger<$safeitemname$> logger;
        private readonly IMapper mapper;

        public $safeitemname$(ILogger<$safeitemname$> logger, IMapper mapper)
        {
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task Handle($fileinputname$ notification, CancellationToken cancellation)
        {            
        }
    }
}
