using QuokkaDev.Templates.Domain.Common.Keys;
using QuokkaDev.Templates.Domain.SeedWork;

namespace QuokkaDev.Templates.Domain.Common.Events
{
    public sealed record HelloWorldMessageCreatedEvent(HelloWorldMessageId aggregateId) : IDomainEvent
    {
    }
}
