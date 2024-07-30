using FluentAssertions;
using NetArchTest.Rules;
using QuokkaDev.Templates.ArchitecturalTests.CustomRules;
using QuokkaDev.Templates.Domain.SeedWork;
using Xunit;

namespace QuokkaDev.Templates.ArchitecturalTests.Domain
{
    public class DomainInterfaceTest
    {
        private const string AGGREGATE_NAMESPACE = "QuokkaDev.Templates.Domain";

        [Fact(DisplayName = "Interfaces Should Have Right Name")]
        public void Interfaces_Should_Have_Right_Name()
        {
            var result = Types.InAssembly(typeof(IAggregateRoot).Assembly)
                .That()
                .AreInterfaces()
                .Should()
                .HaveNameStartingWith("I")
                .GetResult();

            result.IsSuccessful.Should().BeTrue($"Interfaces name should be start with 'I' but {result.GetOffendingTypes()} does not");
        }

        [Fact(DisplayName = "No Repository Implementations Are Allowed In Domain")]
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

        [Fact(DisplayName = "Repositories Interfaces Should Have Right Name")]
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

        [Fact(DisplayName = "Repositories Interfaces Should Reside In Right Namespace")]
        public void Repositories_Interfaces_Should_Reside_In_Right_Namespace()
        {
            var result = Types.InAssembly(typeof(IAggregateRoot).Assembly)
                .That()
                .ImplementInterface(typeof(IRepository<>))
                .Should()
                .MeetCustomRule(new RepositoryInterfaceIsInAggregateNamespace())
                .GetResult();

            result.IsSuccessful.Should().BeTrue($"Repositories interfaces must have reside in his aggregate namespace but {result.GetOffendingTypes()} does not");
        }
    }
}
