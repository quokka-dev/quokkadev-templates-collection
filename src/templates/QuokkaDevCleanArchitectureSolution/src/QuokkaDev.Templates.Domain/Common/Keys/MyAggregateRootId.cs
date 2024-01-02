using QuokkaDev.Templates.Domain.SeedWork;

namespace QuokkaDev.Templates.Domain.Common.Keys
{
    public sealed record MyAggregateRootId : EntityKey<Guid>
    {
        private MyAggregateRootId(Guid g) : base(g)
        {
        }

        /// <summary>
        /// Create new CustomerId
        /// </summary>
        /// <returns></returns>
        public static MyAggregateRootId NewId()
        {
            return new MyAggregateRootId(Guid.NewGuid());
        }

        public static implicit operator Guid(MyAggregateRootId id) => id.Value;
        public static explicit operator MyAggregateRootId(Guid g) => new MyAggregateRootId(g);
    }
}
