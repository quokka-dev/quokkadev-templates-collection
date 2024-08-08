using QuokkaDev.Templates.Domain.SeedWork;

namespace QuokkaDev.Templates.Application.Infrastructure.Interfaces
{
    public interface IDomainEventsDispatcher
    {
        Task PublishAsync(IDomainEvent domainEvent, CancellationToken cancellationToken);
    }
}
