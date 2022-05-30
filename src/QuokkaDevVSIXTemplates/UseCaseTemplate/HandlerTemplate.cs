using AutoMapper;
using QuokkaDev.CQRS;
using Microsoft.Extensions.Logging;
using $rootnamespace$.$fileinputname$.Commands;
using $rootnamespace$.$fileinputname$.Queries;

namespace $rootnamespace$.$fileinputname$.Handlers
{
    /// <summary>
    ///  A command handler for $fileinputname$Command
    ///  Created by $username$
    /// </summary>
    public class $fileinputname$CommandHandler  : ICommandHandler<$fileinputname$Command, $fileinputname$CommandResult>
    {
        private readonly ILogger<$fileinputname$CommandHandler> logger;
        private readonly IMapper mapper;

        public $fileinputname$CommandHandler(ILogger<$fileinputname$CommandHandler> logger, IMapper mapper)
        {
            this.logger = logger;
            this.mapper = mapper;
        }

        public Task<$fileinputname$CommandResult> Handle($fileinputname$Command request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    ///  A query handler for $fileinputname$Query
    ///  Created by $username$
    /// </summary>
    public class $fileinputname$QueryHandler: IQueryHandler <$fileinputname$Query, $fileinputname$QueryResult >
    {
        private readonly ILogger<$fileinputname$QueryHandler> logger;
        private readonly IMapper mapper;

        public $fileinputname$QueryHandler(ILogger <$fileinputname$QueryHandler > logger, IMapper mapper)
        {
            this.logger = logger;
            this.mapper = mapper;
        }

        public Task<$fileinputname$QueryResult> Handle($fileinputname$Query request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
