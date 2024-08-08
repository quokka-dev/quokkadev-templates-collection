using QuokkaDev.Templates.Application.Infrastructure.Interfaces;

namespace QuokkaDev.Templates.Api.Infrastructure.Services
{
    public class HttpContextUserAccessor : ICurrentUserAccessor
    {
        private readonly IHttpContextAccessor contextAccessor;

        public HttpContextUserAccessor(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }

        public Task<string> GetCurrentUserNameAsync()
        {
            return Task.FromResult(contextAccessor.HttpContext?.User.Identity?.Name ?? "");
        }
    }
}
