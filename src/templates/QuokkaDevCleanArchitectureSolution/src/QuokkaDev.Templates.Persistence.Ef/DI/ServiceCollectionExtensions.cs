using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using QuokkaDev.Templates.Application.Services;
using QuokkaDev.Templates.Persistence.Ef.Services;
using System.Reflection;

namespace QuokkaDev.Templates.Persistence.Ef.DI
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Register services for data access used in commands
        /// </summary>
        /// <param name="services">The service collection where register the services</param>
        /// <param name="connectionString">the connection string for a SQL server Db</param>
        /// <param name="useInMemoryDb">Indicate if you want to use an inMemory Db (for test purposes)</param>
        /// <returns>The original service collection</returns>
        public static IServiceCollection AddPersistenceEf(this IServiceCollection services, string? connectionString = null, bool useInMemoryDb = false)
        {
            services.AddScoped<IDomainEventsDispatcher, DomainEventDispatcher>()
                .RegisterUnitOfWork(connectionString, useInMemoryDb)
                .RegisterRepositories();

            return services;
        }

        private static IServiceCollection RegisterUnitOfWork(this IServiceCollection services, string? connectionString = null, bool useInMemoryDb = false)
        {
            if (connectionString == null && useInMemoryDb == false)
            {
                throw new ArgumentException("Connection string cannot be null if you don't use inMemory Db");
            }

            if (!useInMemoryDb)
            {
                services.AddDbContext<IUnitOfWork, CommandsDbContext>((sp, options) =>
                {
                    options.UseSqlServer(connectionString!,
                    serverOptions =>
                    {
                        serverOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null);
                    });

                }, ServiceLifetime.Scoped);

            }
            else
            {
                services.AddDbContext<IUnitOfWork, CommandsDbContext>(options =>
                {
                    options.UseInMemoryDatabase("myDataBase");
                    options.ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning));
                }, ServiceLifetime.Scoped);
            }

            return services;
        }

        private static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            Assembly currentAssembly = typeof(ServiceCollectionExtensions).Assembly;

            var repositories = currentAssembly.GetTypes().Where(t => t.IsAbstract == false &&
                    t.Name != "BaseRepository" &&
                    t.Name.EndsWith("Repository")); ;

            foreach (var repository in repositories)
            {
                var repositoryInterface = repository.GetInterface($"I{repository.Name}");
                if (repositoryInterface != null)
                {
                    services.AddScoped(repositoryInterface, repository);
                }
            }

            return services;
        }
    }
}
