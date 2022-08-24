using QuokkaDev.Templates.Domain.Interfaces;
using QuokkaDev.Templates.Domain.SeedWork;

namespace QuokkaDev.Templates.Domain.Aggregates.MyAggregateRoot
{
    public class MyAggregateRoot : Entity, IAggregateRoot
    {
        public MyAggregateRoot()
        {
            this.AddDomainEvent(new MyAggregateRootCreatedEvent());
        }

        public int Id { get; set; }
    }
}
