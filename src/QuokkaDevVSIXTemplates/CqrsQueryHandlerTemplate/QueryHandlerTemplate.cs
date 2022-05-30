using AutoMapper;
using QuokkaDev.CQRS;
using Microsoft.Extensions.Logging;

namespace $rootnamespace$
{
    /// <summary>
    ///  A query handler for $fileinputname$Query
    ///  Created by $username$
    /// </summary>
    public class $safeitemname$ : IQueryHandler <$fileinputname$Query, $fileinputname$QueryResult>
    {
        private readonly ILogger<$safeitemname$> logger;
        private readonly IMapper mapper;

        public $safeitemname$(ILogger<$safeitemname$> logger, IMapper mapper)
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
