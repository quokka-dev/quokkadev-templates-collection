using FluentAssertions;
using NetArchTest.Rules;
using Xunit;

namespace QuokkaDev.Templates.ArchitecturalTests.Infrastructure
{
    public class DependencyTest
    {
        [Fact(DisplayName = "Query.Dapper Should Have No Dependency On Other Projects")]
        public void Query_Dapper_Should_Have_No_Dependency_On_Other_Projects()
        {
            var result = Types.InAssembly(typeof(QuokkaDev.Templates.Query.Dapper.DI.ServiceCollectionExtensions).Assembly)
                .Should()
                .NotHaveDependencyOnAny(
                    "QuokkaDev.Templates.Persistence.Ef",
                    "QuokkaDev.Templates.Api"
                ).GetResult();

            result.IsSuccessful.Should().BeTrue($"QuokkaDev.Templates.Query.Dapper should depend only on Domain and Application but {result.GetOffendingTypes()} does not");
        }

        [Fact(DisplayName = "Persistence.Ef Should Have No Dependency On Other Projects")]
        public void Persistence_Ef_Should_Have_No_Dependency_On_Other_Projects()
        {
            var result = Types.InAssembly(typeof(QuokkaDev.Templates.Persistence.Ef.DI.ServiceCollectionExtensions).Assembly)
                .Should()
                .NotHaveDependencyOnAny(
                    "QQuokkaDev.Templates.Query.Dapper",
                    "QuokkaDev.Templates.Api"
                ).GetResult();

            result.IsSuccessful.Should().BeTrue($"QuokkaDev.Templates.Persistence.Ef should depend only on Domain and Application but {result.GetOffendingTypes()} does not");
        }
    }
}
