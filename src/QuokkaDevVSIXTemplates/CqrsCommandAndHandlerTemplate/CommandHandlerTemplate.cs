using AutoMapper;
using QuokkaDev.CQRS;
using Microsoft.Extensions.Logging;

namespace $rootnamespace$
{
    /// <summary>
    /// A command request
    /// </summary>
    public class $fileinputname$Command
    {

    }

    /// <summary>
    /// A command result
    /// </summary>
    public class $fileinputname$CommandResult
    {

    }

    /// <summary>
    ///  A command handler for $fileinputname$Command
    /// </summary>
    public class $safeitemname$  : ICommandHandler<$fileinputname$Command, $fileinputname$CommandResult>
    {
        private readonly ILogger<$safeitemname$> logger;
        private readonly IMapper mapper;

        public $safeitemname$ (ILogger<$safeitemname$> logger, IMapper mapper)
        {
            this.logger = logger;
            this.mapper = mapper;
        }

        public Task<$fileinputname$CommandResult> Handle($fileinputname$Command request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
