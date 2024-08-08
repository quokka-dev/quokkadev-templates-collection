using Microsoft.Extensions.DependencyInjection;

namespace QuokkaDev.Templates.Application.Infrastructure.Services
{
    internal class AutoRegisterAttribute : Attribute
    {
        public ServiceLifetime Lifetime { get; }

        public AutoRegisterAttribute(ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            Lifetime = lifetime;
        }
    }
}
