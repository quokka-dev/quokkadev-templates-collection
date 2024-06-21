using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace QuokkaDev.Templates.Api.Infrastructure.Filters
{
    public class ValidateModelFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            ControllerBase? controller = context.Controller as ControllerBase;
            if (!controller!.ModelState.IsValid)
            {
                context.Result = controller.BadRequest(controller.ModelState);
            }
            else
            {
                await next();
            }
        }
    }
}
