using QuokkaDev.Templates.Domain.Common.Events;
using QuokkaDev.Templates.Domain.Common.Keys;

namespace QuokkaDev.Templates.Domain.Samples.HelloWorldMessages
{
    public static class HelloWorldMessageFactory
    {
        public static HelloWorldMessage CreateNewHelloWorldMessage(string message)
        {
            HelloWorldMessage newAggregate = new HelloWorldMessage()
            {
                Id = HelloWorldMessageId.NewId(),
                Message = message
            };

            newAggregate.AddDomainEvent(new HelloWorldMessageCreatedEvent(newAggregate.Id));

            return newAggregate;
        }
    }
}
