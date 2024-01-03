using Microsoft.Extensions.DependencyInjection;
using QuokkaDev.Templates.Application.Services;
using System.Reflection;

namespace QuokkaDev.Templates.Query.Dapper.DI
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Register services used in queries
        /// </summary>
        /// <param name="services">The service collection where register the services</param>
        /// <param name="connectionString">the connection string for a SQL server Db</param>        
        /// <returns>The original service collection</returns>
        public static IServiceCollection AddQueriesDataAccess(this IServiceCollection services, string connectionString)
        {
            Assembly currentAssembly = typeof(ServiceCollectionExtensions).Assembly;

            var queryServices = currentAssembly.GetTypes().Where(t => "QuokkaDev.Templates.Query.Dapper.Services".Equals(t.Namespace!) &&
                    t.IsAbstract == false &&
                    t.IsAssignableTo(typeof(IQueryService)) &&
                    t.Name.EndsWith("QueryService")); ;

            foreach (var queryService in queryServices)
            {
                var queryServiceInterface = queryService.GetInterface($"I{queryService.Name}");
                if (queryServiceInterface != null)
                {
                    services.AddScoped(queryServiceInterface, queryService);
                }
            }

            services.AddSingleton<QuerySettings>(new QuerySettings(connectionString));

            return services;
        }
    }
}
