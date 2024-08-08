using QuokkaDev.Templates.Domain.Common.Keys;

namespace QuokkaDev.Templates.Domain.Samples.HelloWorldMessages
{
    /// <summary>
    /// Represent a HelloWorldMessage
    /// </summary>
    public class HelloWorldMessage : BaseAggregateRoot<HelloWorldMessageId>
    {
        public required string Message { get; init; }

        internal HelloWorldMessage()
        {
        }
    }
}
