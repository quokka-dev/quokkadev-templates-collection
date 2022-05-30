using QuokkaDev.AsyncNotifications.Abstractions;
using QuokkaDev.Templates.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuokkaDev.Templates.DataAccess.Commands.Services
{
    internal class DomainEventDispatcher : IDomainEventsDispatcher
    {
        private readonly IAsyncNotificationDispatcher notificationDispatcher;

        public DomainEventDispatcher(IAsyncNotificationDispatcher notificationDispatcher)
        {
            this.notificationDispatcher = notificationDispatcher;
        }

        public Task Publish(IDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            return notificationDispatcher.DispatchAsync(domainEvent, cancellationToken);
        }
    }
}
