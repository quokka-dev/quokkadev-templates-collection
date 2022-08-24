using QuokkaDev.AsyncNotifications.Abstractions;
using QuokkaDev.Templates.Domain.Interfaces;
using QuokkaDev.Templates.Domain.SeedWork;
using System.Reflection;

namespace QuokkaDev.Templates.DataAccess.Commands.Services
{
    internal class DomainEventDispatcher : IDomainEventsDispatcher
    {
        private readonly IAsyncNotificationDispatcher notificationDispatcher;
        private static MethodInfo dispatchAsync => GetDispatchAsyncMethodInfo();

        public DomainEventDispatcher(IAsyncNotificationDispatcher notificationDispatcher)
        {
            this.notificationDispatcher = notificationDispatcher;
        }

        public Task Publish(IDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            return CallGenericMethod(domainEvent, cancellationToken);
        }
        public Task Notify(IDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            if (GetNotification(domainEvent) is object notification)
            {
                return CallGenericMethod(notification, cancellationToken);
            }
            else
            {
                return Task.CompletedTask;
            }
        }

        private object? GetNotification(IDomainEvent domainEvent)
        {
            Type domainEvenNotificationType = typeof(DomainEventNotification<>);
            var domainEventNotificationWithGenericType = domainEvenNotificationType.MakeGenericType(domainEvent.GetType());
            return Activator.CreateInstance(domainEventNotificationWithGenericType, domainEvent);
        }

        private Task CallGenericMethod(object parameter, CancellationToken cancellationToken)
        {
            var genericMethod = dispatchAsync.MakeGenericMethod(parameter.GetType());
            if (genericMethod is null)
            {
                throw new InvalidOperationException($"Cannot generate generic method for ${nameof(IAsyncNotificationDispatcher.DispatchAsync)}");
            }
            return (Task)genericMethod.Invoke(notificationDispatcher, new object[] { parameter, cancellationToken })!;
        }

        private static MethodInfo GetDispatchAsyncMethodInfo()
        {
            Type t = typeof(IAsyncNotificationDispatcher);
            return t.GetMethods().First(m => m.Name == nameof(IAsyncNotificationDispatcher.DispatchAsync) && m.GetParameters().Length == 2);
        }
    }
}
