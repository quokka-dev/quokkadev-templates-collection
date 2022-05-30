using QuokkaDev.Templates.Domain.Aggregates.MyAggregateRoot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuokkaDev.Templates.DataAccess.Commands.Repositories
{
    internal class MyAggregateRootRepository : BaseRepository<MyAggregateRoot>, IMyAggregateRootRepository
    {
        public MyAggregateRootRepository(CommandsDbContext context) : base(context)
        {
        }
    }
}
