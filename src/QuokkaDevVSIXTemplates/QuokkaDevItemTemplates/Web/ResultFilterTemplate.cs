using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace $rootnamespace$
{
    /// <summary>
    /// $fileinputname$ result filter    
    /// </summary>    
    public class $fileinputname$ : IAsyncResultFilter
    {        
        private readonly ILogger<$fileinputname$> logger;

        public $fileinputname$(ILogger<$fileinputname$> logger)
        {            
            this.logger = logger;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {            
            await next();            
        }
    }
}