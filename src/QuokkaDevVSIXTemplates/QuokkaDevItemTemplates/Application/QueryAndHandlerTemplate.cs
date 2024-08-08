using AutoMapper;
using QuokkaDev.Cqrs.Abstractions;
using Microsoft.Extensions.Logging;

namespace $rootnamespace$
{
    /// <summary>
    /// A query request
    /// </summary>
    public class $fileinputname$Query
    {

    }

    /// <summary>
    /// A query result
    /// </summary>
    public class $fileinputname$QueryResult
    {

    }

    /// <summary>
    ///  A query handler for $fileinputname$Query
    /// </summary>
    public class $safeitemname$: IQueryHandler <$fileinputname$Query, $fileinputname$QueryResult>
    {
        private readonly ICoreServices<$safeitemname$> _coreServices;

        public $safeitemname$(ICoreServices<$safeitemname$> coreServices)
        {
            _coreServices = coreServices;
        }

        public Task<$fileinputname$QueryResult> Handle($fileinputname$Query request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
