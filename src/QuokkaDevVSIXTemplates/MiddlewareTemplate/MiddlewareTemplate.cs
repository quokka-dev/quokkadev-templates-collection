using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace $rootnamespace$
{
    public class $safeitemname$
    {
        private readonly RequestDelegate next;
        
        private readonly ILogger<$safeitemname$> logger;

        public $safeitemname$(RequestDelegate next, ILogger<$safeitemname$> logger)
        {
            this.next = next;            
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            await next(httpContext);
        }
    }
}
