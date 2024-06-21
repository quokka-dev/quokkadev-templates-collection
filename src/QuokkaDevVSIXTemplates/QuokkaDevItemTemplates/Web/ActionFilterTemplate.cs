using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace $rootnamespace$
{
    /// <summary>
    /// $fileinputname$ action filter    
    /// </summary>    
    public class $fileinputname$ : IAsyncActionFilter
    {        
        private readonly ILogger<$fileinputname$> logger;

        public $fileinputname$(ILogger<$fileinputname$> logger)
        {            
            this.logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if(true)
            {
                await next();
            }
            else
            {
                context.Result = new ObjectResult("");
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
            }
        }
    }
}