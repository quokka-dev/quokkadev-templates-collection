using Microsoft.Extensions.DependencyInjection;

namespace QuokkaDev.Package
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMyFeature(this IServiceCollection services)
        {
            // Register package services here
            return services;
        }
    }
}
