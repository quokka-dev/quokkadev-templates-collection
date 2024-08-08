namespace QuokkaDev.Templates.Api.Infrastructure.Middlewares
{
    internal class CorrelationMiddleware
    {
        private readonly RequestDelegate next;
        private readonly CorrelationOptions options;
        private readonly ILogger<CorrelationMiddleware> logger;

        public CorrelationMiddleware(RequestDelegate next, CorrelationOptions options, ILogger<CorrelationMiddleware> logger)
        {
            this.next = next;
            this.options = options;
            this.logger = logger;
        }

        public Task InvokeAsync(HttpContext httpContext, CorrelationService correlationService)
        {
            if (httpContext == null)
            {
                throw new ArgumentException("", nameof(httpContext));
            }

            return InvokeAsyncInternal(httpContext, correlationService);
        }

        private async Task InvokeAsyncInternal(HttpContext httpContext, CorrelationService correlationService)
        {
            var (correlationID, headerName) = GetCorrelationId(httpContext);
            correlationService?.SetCorrelationID(correlationID);

            if (options.WriteCorrelationIDToResponse)
            {
                httpContext.Response.Headers.Append(headerName, correlationID);
            }

            if (options.EnrichLog)
            {
                using (logger.BeginScope(GetLogEnrichmentData(correlationID)))
                {
                    await next(httpContext).ConfigureAwait(false);
                }
            }
            else
            {
                await next(httpContext).ConfigureAwait(false);
            }
        }

        private Dictionary<string, object> GetLogEnrichmentData(string correlationID)
        {
            var loggerState = new Dictionary<string, object>
            {
                { options.LogPropertyName, correlationID }
            };
            return loggerState;
        }

        private (string CorrelationID, string HeaderName) GetCorrelationId(HttpContext httpContext)
        {
            string correlationId = "";
            string headerName = "";
            if (options.TryToUseRequestHeader)
            {
                foreach (string validRequestHeader in options.ValidRequestHeaders)
                {
                    if (httpContext.Request.Headers.ContainsKey(validRequestHeader))
                    {
                        correlationId = "" + httpContext.Request.Headers[validRequestHeader];
                        headerName = validRequestHeader;
                        break;
                    }
                }
            }
            if (string.IsNullOrWhiteSpace(correlationId))
            {
                //If no correlation id is found in the request a new one is generated.
                correlationId = Guid.NewGuid().ToString();
                headerName = options.DefaultResponseHeaderName;
            }
            return (correlationId, headerName);
        }
    }

    public class CorrelationService
    {
        private string correlationID = "";

        public void SetCorrelationID(string id)
        {
            if (string.IsNullOrWhiteSpace(correlationID))
            {
                correlationID = id;
            }
        }

        public string GetCurrentCorrelationID()
        {
            return correlationID;
        }
    }

    internal class CorrelationOptions
    {
        public bool TryToUseRequestHeader { get; set; }
        public IEnumerable<string> ValidRequestHeaders { get; set; } = new List<string>();
        public bool EnrichLog { get; set; }
        public string LogPropertyName { get; set; } = Constants.DEFAULT_CORRELATION_LOG_PROPERTY;
        public bool WriteCorrelationIDToResponse { get; set; }
        public string DefaultResponseHeaderName { get; set; } = Constants.DEFAULT_CORRELATION_HEADER_NAME;
    }

    internal static class Constants
    {
        public const string DEFAULT_CORRELATION_HEADER_NAME = "X-Correlation-Id";
        public const string DEFAULT_CORRELATION_LOG_PROPERTY = "CorrelationId";
    }
}
