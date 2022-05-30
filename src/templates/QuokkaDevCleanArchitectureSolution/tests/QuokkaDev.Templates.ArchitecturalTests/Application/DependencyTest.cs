using FluentAssertions;
using QuokkaDev.Templates.Application.DI;
using NetArchTest.Rules;
using Xunit;

namespace QuokkaDev.Templates.ArchitecturalTests.Application
{
    public class DependencyTest
    {
        [Fact]
        public void Application_Should_Have_No_Dependency_On_Other_Projects_But_Domain()
        {
            var result = Types.InAssembly(typeof(ServiceCollectionExtensions).Assembly)
                .Should()
                .NotHaveDependencyOnAny(
                    "QuokkaDev.Templates.DataAccess.Commands",
                    "QuokkaDev.Templates.DataAccess.Queries",
                    "QuokkaDev.Templates.Api"
                ).GetResult();

            result.IsSuccessful.Should().BeTrue($"Application should depend only on Domain but {result.GetOffendingTypes()} does not");
        }
    }
}
