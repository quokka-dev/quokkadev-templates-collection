using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuokkaDev.AsyncNotifications;
using QuokkaDev.Cqrs;
using QuokkaDev.Cqrs.Decorators;
using QuokkaDev.Templates.Application.Infrastructure.Interfaces;
using QuokkaDev.Templates.Application.Infrastructure.Services;
using System.Reflection;

namespace QuokkaDev.Templates.Application
{
    public static class ServiceCollectionExtensions
    {
        public static ApplicationServicesBuilder AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IRepositoryFactory, RepositoryFactory>();
            services.AddScoped(typeof(ICoreServices<>), typeof(CoreServices<>));
            return new ApplicationServicesBuilder(services, configuration);
        }
    }

    public class ApplicationServicesBuilder
    {
        private readonly IServiceCollection _services;
        private readonly IConfiguration _configuration;

        public ApplicationServicesBuilder(IServiceCollection services, IConfiguration configuration)
        {
            _services = services;
            _configuration = configuration;
        }

        public ApplicationServicesBuilder AutoRegisterApplicationservicesServices()
        {
            Assembly assembly = typeof(ServiceCollectionExtensions).Assembly;

            var appServices = assembly.GetTypes().Where(t => t.CustomAttributes.Any(a => a.AttributeType == typeof(AutoRegisterAttribute)));

            foreach (var appService in appServices)
            {
                var serviceInterface = appService.GetInterfaces().SingleOrDefault();
                if (serviceInterface != null)
                {
                    var lifetime = appService.GetCustomAttribute<AutoRegisterAttribute>()!.Lifetime;
                    _services.Add(new ServiceDescriptor(serviceInterface, appService, lifetime));
                }
            }
            return this;
        }

        public ApplicationServicesBuilder AddDomainEventsDispatchment()
        {
            _services.AddAsyncNotifications(typeof(ServiceCollectionExtensions).Assembly);
            _services.AddScoped<IDomainEventsDispatcher, DomainEventDispatcher>();
            return this;
        }

        public ApplicationServicesBuilder AddAutoMapper()
        {
            _services.AddAutoMapper([typeof(ServiceCollectionExtensions).Assembly]);
            return this;
        }

        public ApplicationServicesBuilder RegisterBatches()
        {
            _services.AddSingleton<BatchService>();
            _services.AddSingleton<IBatchQueueService, BatchService>(sp => sp.GetRequiredService<BatchService>());
            _services.AddSingleton<IBatchProcessor>(sp => sp.GetRequiredService<BatchService>());

            var batchTypes = typeof(ServiceCollectionExtensions).Assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IBatch<>)));

            foreach (var batchType in batchTypes)
            {
                _services.AddTransient(batchType);
            }

            return this;
        }

        public ApplicationServicesBuilder AddCommandAndQueries()
        {
            _services.AddTransient<ICommandService, CommandService>();
            _services.AddCQRS(typeof(ServiceCollectionExtensions).Assembly);

            _services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly);

            _services.AddCommandLogging(o =>
            {
                o.IsRequestLoggingEnabled = true;
                o.IsResponseLoggingEnabled = true;
            }).AndCommandValidation(_ => { });

            _services.AddQueryLogging(o =>
            {
                o.IsRequestLoggingEnabled = true;
                o.IsResponseLoggingEnabled = true;
            }).AndQueryValidation(_ => { });

            return this;
        }
    }
}
