using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuokkaDev.Templates.Domain.Interfaces
{
    public interface IDomainEventsDispatcher
    {
        Task Publish(IDomainEvent domainEvent, CancellationToken cancellationToken);
    }
}
