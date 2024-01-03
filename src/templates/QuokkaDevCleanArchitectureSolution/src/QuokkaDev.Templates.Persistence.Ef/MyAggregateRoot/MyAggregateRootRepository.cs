using QuokkaDev.Templates.Domain.MyAggregateRoot;


namespace QuokkaDev.Templates.Persistence.Ef.MyAggregateRoot
{
    internal class MyAggregateRootRepository : BaseRepository<Domain.MyAggregateRoot.MyAggregateRoot>, IMyAggregateRootRepository
    {
        public MyAggregateRootRepository(CommandsDbContext context) : base(context)
        {
        }
    }
}
