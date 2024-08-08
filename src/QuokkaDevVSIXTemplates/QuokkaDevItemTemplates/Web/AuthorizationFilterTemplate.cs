using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace $rootnamespace$
{
    public class $safeitemname$ : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private readonly ILogger<$safeitemname$> logger;

        public $safeitemname$(ILogger<$safeitemname$> logger)
        {
            this.logger = logger;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (true) // check if authorized 
            {
                context.Result = new UnauthorizedResult();
            }            
        }
    }
}
