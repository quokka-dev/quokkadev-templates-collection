namespace QuokkaDev.Templates.Domain.Interfaces
{
    public interface IDomainEventsDispatcher
    {
        Task Publish(IDomainEvent domainEvent, CancellationToken cancellationToken);
        Task Notify(IDomainEvent domainEvent, CancellationToken cancellationToken);
    }
}
