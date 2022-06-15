namespace $rootnamespace$
{
    internal class $safeitemname$ : DbContext, IUnitOfWork
    {
        private readonly IDomainEventsDispatcher eventsDispatcher;

/// <summary>
/// Default constructor
/// </summary>
public $safeitemname$()
        {
            eventsDispatcher = null!;
        }

/// <summary>
/// Constructor
/// </summary>
/// <param name="context">Configuration options</param>
public $safeitemname$(DbContextOptions <$safeitemname$> context, IDomainEventsDispatcher eventsDispatcher) : base(context)
        {
    this.eventsDispatcher = eventsDispatcher;
}

#region Db Sets

//public DbSet<MyAggregateRoot> MyAggregateRoots => Set<MyAggregateRoot>();

#endregion

/// <summary>
/// Configure EF model
/// </summary>
/// <param name="modelBuilder"></param>
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    //modelBuilder.Entity<MyAggregateRoot>(ConfigureMyAggregateRoot);
    //modelBuilder.ApplyConfigurationsFromAssembly(typeof($safeitemname$).Assembly);
}

/// <summary>
/// Save entitities
/// </summary>
/// <param name="cancellationToken"></param>
/// <returns></returns>
/// <exception cref="NotImplementedException"></exception>
public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
{
    await DispatchDomainEventsAsync(cancellationToken);
    var result = await base.SaveChangesAsync();

    return true;
}

private async Task DispatchDomainEventsAsync(CancellationToken cancellationToken = default)
{
    var domainEntities = this.ChangeTracker
        .Entries<Entity>()
        .Where(x => x.Entity.DomainEvents?.Any() == true);

    var domainEvents = domainEntities
        .SelectMany(x => x.Entity.DomainEvents!)
        .ToList();

    domainEntities.ToList()
        .ForEach(entity => entity.Entity.DomainEvents!.Clear());

    var tasks = domainEvents
        .Select(async (domainEvent) =>
        {
            await eventsDispatcher.Publish(domainEvent, cancellationToken);
        });

    await Task.WhenAll(tasks);
}
    }
}
