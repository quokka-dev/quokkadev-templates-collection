using QuokkaDev.Templates.Domain.SeedWork;

namespace QuokkaDev.Templates.Application.Services
{
    public interface IDomainEventsDispatcher
    {
        Task Publish(IDomainEvent domainEvent, CancellationToken cancellationToken);
    }
}
