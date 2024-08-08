using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using QuokkaDev.Templates.Api.Infrastructure.Models;
using System.Net;

namespace QuokkaDev.Templates.Api.Infrastructure.Filters
{
    public class GeneralExceptionFilter : IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            ErrorViewModel error = new(context.Exception);
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<GeneralExceptionFilter>>();
            logger.LogError(context.Exception, "{message}", context.Exception.Message);

            switch (context.Exception)
            {
                //case EntityNotFoundException:
                //    context.Result = new NotFoundObjectResult(error);
                //    break;
                //case InvalidQueryRequestException:
                //    context.Result = new BadRequestObjectResult(error);
                //    break;
                default:
                    context.Result = new ObjectResult(error);
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
            context.ExceptionHandled = true;
            return Task.CompletedTask;
        }
    }
}
