using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using QuokkaDev.AsyncNotifications;
using QuokkaDev.Cqrs;
using QuokkaDev.Cqrs.Decorators;
using QuokkaDev.Templates.Application.Infrastructure.Exceptions;

namespace QuokkaDev.Templates.Application.DI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddCQRS(typeof(ServiceCollectionExtensions).Assembly);

            services.AddAsyncNotifications(typeof(ServiceCollectionExtensions).Assembly);

            services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly);

            services.AddCommandLogging(o =>
            {
                o.IsRequestLoggingEnabled = true;
                o.IsResponseLoggingEnabled = true;
            }).AndCommandValidation(opts => opts.CustomExceptionType = typeof(InvalidQueryRequestException));

            services.AddQueryLogging(o =>
            {
                o.IsRequestLoggingEnabled = true;
                o.IsResponseLoggingEnabled = true;
            }).AndQueryValidation(opts => opts.CustomExceptionType = typeof(InvalidQueryRequestException));

            services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);

            return services;
        }
    }
}
