using Microsoft.Extensions.DependencyInjection;
using QuokkaDev.Templates.Application.Infrastructure.Interfaces;
using QuokkaDev.Templates.Domain.SeedWork;

namespace QuokkaDev.Templates.Application.Infrastructure.Services
{
    /// <summary>
    /// Service for creating repository
    /// </summary>
    internal class RepositoryFactory : IRepositoryFactory
    {
        private readonly IServiceProvider serviceProvider;

        public RepositoryFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public TRepository GetRepository<TRepository>() where TRepository : IRepository
        {
            return serviceProvider.GetRequiredService<TRepository>();
        }
    }
}
