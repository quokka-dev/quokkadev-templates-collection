using FluentAssertions;
using NetArchTest.Rules;
using QuokkaDev.Templates.Domain.SeedWork;
using Xunit;

namespace QuokkaDev.Templates.ArchitecturalTests.Infrastructure
{
    public class RepositoryTest
    {
        private const string REPOSITORY_NAMESPACE = "QuokkaDev.Templates.DataAccess.Commands.*";

        [Fact]
        public void Repositories_Should_Reside_In_Namespace()
        {
            var result = Types.InAssembly(typeof(QuokkaDev.Templates.DataAccess.Commands.DI.ServiceCollectionExtensions).Assembly)
                .That()
                .ImplementInterface(typeof(IRepository<>))
                .Should()
                .ResideInNamespaceMatching(REPOSITORY_NAMESPACE)
                .GetResult();

            result.IsSuccessful.Should().BeTrue($"Repositories should reside in namespaces {REPOSITORY_NAMESPACE} but {result.GetOffendingTypes()} does not");
        }

        [Fact]
        public void Repositories_Should_Have_Right_Name()
        {
            var result = Types.InAssembly(typeof(QuokkaDev.Templates.DataAccess.Commands.DI.ServiceCollectionExtensions).Assembly)
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
