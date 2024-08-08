using AutoMapper;
using QuokkaDev.Cqrs.Abstractions;
using Microsoft.Extensions.Logging;
using QuokkaDev.Templates.Application.Infrastructure.Interfaces;

namespace $rootnamespace$
{
    /// <summary>
    ///  A command handler for $fileinputname$Command
    /// </summary>
    public class $safeitemname$  : ICommandHandler<$fileinputname$Command, $fileinputname$CommandResult>
    {
        private readonly ICommandsCoreServices<$safeitemname$> _coreServices;

        public $safeitemname$(ICommandsCoreServices<$safeitemname$> coreServices)
        {
            _coreServices = coreServices;
        }

        public Task<$fileinputname$CommandResult> Handle($fileinputname$Command request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
