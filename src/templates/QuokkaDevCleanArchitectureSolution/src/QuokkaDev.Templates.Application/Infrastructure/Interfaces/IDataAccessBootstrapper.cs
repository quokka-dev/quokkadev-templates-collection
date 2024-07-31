using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuokkaDev.Templates.Application.Infrastructure.Interfaces
{
    public interface IDataAccessBootstrapper : IDisposable
    {
        Task BootstrapAsync();
    }
}
