using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using QuokkaDev.Templates.Application.Infrastructure.Interfaces;
using QuokkaDev.Templates.Persistence.Ef.Infrastructure.Implementations;
using QuokkaDev.Templates.Persistence.Ef.Infrastructure.Interceptors;
using QuokkaDev.Templates.Persistence.Ef.Infrastructure.Utils;
using System.Reflection;

namespace QuokkaDev.Templates.Persistence.Ef
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
        public static IServiceCollection AddDataAccess(this IServiceCollection services, string connectionString, bool enableAudit = true, bool enableDomainEventDispathing = true)
        {
            services.RegisterUnitOfWork(connectionString, enableAudit, enableDomainEventDispathing)
                .RegisterRepositories();

            return services;
        }

        private static IServiceCollection RegisterUnitOfWork(this IServiceCollection services, string connectionString, bool enableAudit = true, bool enableDomainEventDispathing = true)
        {

            services.AddDbContext<IUnitOfWork, ApplicationDbContext>((sp, options) =>
            {
                options.UseSqlServer(connectionString,
                serverOptions =>
                {
                    serverOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null);
                    serverOptions.MigrationsAssembly(Constants.MIGRATION_ASSEMBLY);
                });

                List<IInterceptor> interceptors = new List<IInterceptor>();
                if (enableAudit)
                {
                    interceptors.Add(sp.GetRequiredService<AuditInterceptor>());
                }
                if (enableDomainEventDispathing)
                {
                    interceptors.Add(sp.GetRequiredService<DispatchEventInterceptor>());
                }

                if (interceptors.Count > 0)
                {
                    options.AddInterceptors(interceptors.ToArray());
                }

            }, ServiceLifetime.Scoped);

            services.AddScoped<DbContext>(sp => { return sp.GetRequiredService<ApplicationDbContext>(); });
            services.AddScoped<ITransactionManager>(sp => { return sp.GetRequiredService<ApplicationDbContext>(); });

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
