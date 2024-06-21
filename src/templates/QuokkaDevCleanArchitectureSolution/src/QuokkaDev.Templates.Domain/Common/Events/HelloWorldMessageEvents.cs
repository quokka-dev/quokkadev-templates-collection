using QuokkaDev.Templates.Domain.Common.Keys;

namespace QuokkaDev.Templates.Domain.Common.Events
{
    public sealed record HelloWorldMessageCreatedEvent(HelloWorldMessageId aggregateId) : IDomainEvent
    {
    }
}
