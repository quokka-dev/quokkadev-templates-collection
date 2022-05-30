using FluentAssertions;
using QuokkaDev.Templates.Domain.Interfaces;
using NetArchTest.Rules;
using Xunit;

namespace QuokkaDev.Templates.ArchitecturalTests.Domain
{
    public class DomainEventsTest
    {
        private const string EVENTS_NAMESPACE = "QuokkaDev.Templates.Domain.Events";

        [Fact]
        public void DomainEvents_Should_Reside_In_Namespace()
        {
            var result = Types.InAssembly(typeof(IAggregateRoot).Assembly)
                .That()
                .ImplementInterface(typeof(IDomainEvent))
                .Should()
                .ResideInNamespace(EVENTS_NAMESPACE)
                .GetResult();

            result.IsSuccessful.Should().BeTrue($"DomainEvents are allowed only in namespace '{EVENTS_NAMESPACE}' but {result.GetOffendingTypes()} does not");
        }

        [Fact]
        public void Only_DomainEvents_Are_Allowed_In_Namespace()
        {
            var result = Types.InAssembly(typeof(IAggregateRoot).Assembly)
                .That()
                .ResideInNamespace(EVENTS_NAMESPACE)
                .Should()
                .ImplementInterface(typeof(IDomainEvent))
                .GetResult();

            result.IsSuccessful.Should().BeTrue($"only DomainEvents are allowed in namespace '{EVENTS_NAMESPACE}' but {result.GetOffendingTypes()} does not");
        }
    }
}
