using FluentAssertions;
using NetArchTest.Rules;
using QuokkaDev.Templates.Domain.SeedWork;
using QuokkaDev.Templates.Persistence.Ef;
using Xunit;

namespace QuokkaDev.Templates.ArchitecturalTests.Infrastructure
{
    public class RepositoryTest
    {
        [Fact]
        public void Repositories_Should_Have_Right_Name()
        {
            var result = Types.InAssembly(typeof(ServiceCollectionExtensions).Assembly)
                .That()
                .ImplementInterface(typeof(IRepository<>))
                .Should()
                .HaveNameEndingWith("Repository")
                .Or()
                .HaveNameEndingWith("Repository`1")
                .GetResult();

            result.IsSuccessful.Should().BeTrue($"Repositories should have name ending with 'Repository' but {result.GetOffendingTypes()} does not");
        }
    }
}
