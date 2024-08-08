using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using QuokkaDev.Templates.Application.Infrastructure.Interfaces;
using QuokkaDev.Templates.Domain.SeedWork;

namespace QuokkaDev.Templates.Persistence.Ef.Infrastructure.Interceptors
{
    internal class DispatchEventInterceptor : ISaveChangesInterceptor
    {
        private readonly IDomainEventsDispatcher _dispatcher;

        public DispatchEventInterceptor(IDomainEventsDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public async ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            await DispatchDomainEventsAsync(eventData.Context, cancellationToken);
            return result;
        }

        public InterceptionResult<int> SavingChanges(
            DbContextEventData eventData,
            InterceptionResult<int> result)
        {
            var task = SavingChangesAsync(eventData, result, default);
            task.GetAwaiter().GetResult();
            return result;
        }

        private async Task DispatchDomainEventsAsync(DbContext? context, CancellationToken cancellationToken = default)
        {
            if (context != null)
            {
                var domainEntities = context.ChangeTracker
                    .Entries<IAggregateRoot>()
                    .Where(x => x.Entity.DomainEvents?.Any() == true);

                var domainEvents = domainEntities
                    .SelectMany(x => x.Entity.DomainEvents!)
                    .ToList();

                domainEntities.ToList()
                    .ForEach(entity => entity.Entity.ClearDomainEvents());

                var tasks = domainEvents
                    .Select(async (domainEvent) => await _dispatcher.PublishAsync(domainEvent, cancellationToken));

                await Task.WhenAll(tasks);
            }
        }
    }
}
