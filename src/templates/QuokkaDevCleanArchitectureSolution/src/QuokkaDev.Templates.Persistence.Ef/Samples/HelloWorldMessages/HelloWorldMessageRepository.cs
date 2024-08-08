using Microsoft.EntityFrameworkCore;
using QuokkaDev.Templates.Domain.Samples.HelloWorldMessages;
using QuokkaDev.Templates.Persistence.Ef.Infrastructure.Implementations;

namespace QuokkaDev.Templates.Persistence.Ef.Samples.HelloWorldMessages
{
    internal class HelloWorldMessageRepository : BaseRepository<HelloWorldMessage>, IHelloWorldMessageRepository
    {
        public HelloWorldMessageRepository(DbContext context) : base(context)
        {
        }
    }
}
