using QuokkaDev.Templates.Domain.Common.Events;
using QuokkaDev.Templates.Domain.Common.Keys;
using QuokkaDev.Templates.Domain.SeedWork;

namespace QuokkaDev.Templates.Domain.MyAggregateRoot
{
    public class MyAggregateRoot : BaseAggregateRoot<MyAggregateRootId>
    {
        public MyAggregateRoot()
        {
            AddDomainEvent(new MyAggregateRootCreatedEvent());
        }
    }
}
