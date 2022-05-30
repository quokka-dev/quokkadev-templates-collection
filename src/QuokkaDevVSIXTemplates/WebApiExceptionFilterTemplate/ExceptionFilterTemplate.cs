using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace $rootnamespace$
{
    /// <summary>
    /// $fileinputname$ exception filter    
    /// </summary>    
    public class $fileinputname$ : IAsyncActionFilter
    {        
        private readonly ILogger<$fileinputname$> logger;

        public $fileinputname$(ILogger<$fileinputname$> logger)
        {            
            this.logger = logger;
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {            
            context.ExceptionHandled = true;
            return Task.CompletedTask;
        }
}
}