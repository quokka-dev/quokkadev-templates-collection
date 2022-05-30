using FluentAssertions;
using QuokkaDev.Templates.Domain.Interfaces;
using NetArchTest.Rules;
using Xunit;

namespace QuokkaDev.Templates.ArchitecturalTests.Domain
{
    public class DomainInterfaceTest
    {
        private const string INTERFACE_NAMESPACE = "QuokkaDev.Templates.Domain.Interfaces";
        private const string AGGREGATE_NAMESPACE = "QuokkaDev.Templates.Domain.Aggregates";

        [Fact]
        public void Objects_In_Interfaces_Namespace_Should_Be_Interface()
        {
            var result = Types.InAssembly(typeof(IAggregateRoot).Assembly)
                .That()
                .ResideInNamespace(INTERFACE_NAMESPACE)
                .Should()
                .BeInterfaces()
                .GetResult();

            result.IsSuccessful.Should().BeTrue($"{INTERFACE_NAMESPACE} namespace allows only interfaces but {result.GetOffendingTypes()} does not");
        }

        [Fact]
        public void Other_Interfaces_Should_Be_Repository()
        {
            var result = Types.InAssembly(typeof(IAggregateRoot).Assembly)
                .That()
                .DoNotResideInNamespace(INTERFACE_NAMESPACE)
                .And()
                .AreInterfaces()
                .Should()
                .ImplementInterface(typeof(IRepository<>))
                .GetResult();

            result.IsSuccessful.Should().BeTrue($"only Repositories interfaces are allowed outside namespace '{INTERFACE_NAMESPACE}' but {result.GetOffendingTypes()} does not");
        }

        [Fact]
        public void No_Repository_Implementations_Are_Allowed_In_Domain()
        {
            var result = Types.InAssembly(typeof(IAggregateRoot).Assembly)
                .That()
                .ImplementInterface(typeof(IRepository<>))
                .Should()
                .BeInterfaces()
                .GetResult();

            result.IsSuccessful.Should().BeTrue($"No IRepository<> implementations are allowed in domain but {result.GetOffendingTypes()} does not");
        }

        [Fact]
        public void Repositories_Interfaces_Should_Have_Right_Name()
        {
            var result = Types.InAssembly(typeof(IAggregateRoot).Assembly)
                .That()
                .ImplementInterface(typeof(IRepository<>))
                .Should()
                .HaveNameStartingWith("I")
                .And()
                .HaveNameEndingWith("Repository")
                .GetResult();

            result.IsSuccessful.Should().BeTrue($"Repositories interfaces must have name starting with 'I' and ending with 'Repository' but {result.GetOffendingTypes()} does not");
        }

        [Fact]
        public void Repositories_Interfaces_Should_Have_Right_Namespace()
        {
            var result = Types.InAssembly(typeof(IAggregateRoot).Assembly)
                .That()
                .ImplementInterface(typeof(IRepository<>))
                .Should()
                .ResideInNamespaceStartingWith(AGGREGATE_NAMESPACE)
                .GetResult();

            result.IsSuccessful.Should().BeTrue($"Repositories interfaces must reside in namespace starting with '{AGGREGATE_NAMESPACE}' but {result.GetOffendingTypes()} does not");
        }
    }
}
