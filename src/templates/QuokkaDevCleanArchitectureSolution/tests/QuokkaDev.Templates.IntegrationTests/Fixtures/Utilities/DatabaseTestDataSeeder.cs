using QuokkaDev.Templates.Persistence.Ef.Infrastructure.Implementations;
using System.Threading.Tasks;

namespace QuokkaDev.Templates.IntegrationTests.Fixtures.Utilities
{
    internal static class DatabaseTestDataSeeder
    {
        public static async Task SeedAsync<TEntryPoint>(ApplicationDbContext commandsDbContext, ApplicationFixture<TEntryPoint> fixture)
             where TEntryPoint : Program
        {
            //TODO: Add seed data for integration tests            
        }
    }
}
