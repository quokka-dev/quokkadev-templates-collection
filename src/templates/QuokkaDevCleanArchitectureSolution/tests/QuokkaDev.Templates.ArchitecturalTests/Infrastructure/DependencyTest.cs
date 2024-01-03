using FluentAssertions;
using NetArchTest.Rules;
using Xunit;

namespace QuokkaDev.Templates.ArchitecturalTests.Infrastructure
{
    public class DependencyTest
    {
        [Fact]
        public void DataAccess_Queries_Should_Have_No_Dependency_On_Other_Projects()
        {
            var result = Types.InAssembly(typeof(QuokkaDev.Templates.DataAccess.Queries.DI.ServiceCollectionExtensions).Assembly)
                .Should()
                .NotHaveDependencyOnAny(
                    "QuokkaDev.Templates.DataAccess.Commands",
                    "QuokkaDev.Templates.Api"
                ).GetResult();

            result.IsSuccessful.Should().BeTrue($"QuokkaDev.Templates.DataAccess.Queries should depend only on Domain and Application but {result.GetOffendingTypes()} does not");
        }

        [Fact]
        public void DataAccess_Commands_Should_Have_No_Dependency_On_Other_Projects()
        {
            var result = Types.InAssembly(typeof(QuokkaDev.Templates.DataAccess.Commands.DI.ServiceCollectionExtensions).Assembly)
                .Should()
                .NotHaveDependencyOnAny(
                    "QuokkaDev.Templates.DataAccess.Queries",
                    "QuokkaDev.Templates.Api"
                ).GetResult();

            result.IsSuccessful.Should().BeTrue($"QuokkaDev.Templates.DataAccess.Commands should depend only on Domain and Application but {result.GetOffendingTypes()} does not");
        }
    }
}
