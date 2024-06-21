using QuokkaDev.Templates.Domain.SeedWork;

namespace QuokkaDev.Templates.Application.Infrastructure.Interfaces
{
    public interface IRepositoryFactory
    {
        TRepository GetRepository<TRepository>() where TRepository : IRepository;
    }
}
