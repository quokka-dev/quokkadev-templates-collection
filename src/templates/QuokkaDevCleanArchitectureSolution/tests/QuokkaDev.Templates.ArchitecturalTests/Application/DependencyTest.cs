using FluentAssertions;
using NetArchTest.Rules;
using QuokkaDev.Templates.Application.DI;
using Xunit;

namespace QuokkaDev.Templates.ArchitecturalTests.Application
{
    public class DependencyTest
    {
        [Fact(DisplayName = "Application Should Have No Dependency On Other Projects But Domain")]
        public void Application_Should_Have_No_Dependency_On_Other_Projects_But_Domain()
        {
            var result = Types.InAssembly(typeof(ServiceCollectionExtensions).Assembly)
                .Should()
                .NotHaveDependencyOnAny(
                    "QuokkaDev.Templates.Persistence.Ef",
                    "QuokkaDev.Templates.Query.Dapper",
                    "QuokkaDev.Templates.Api"
                ).GetResult();

            result.IsSuccessful.Should().BeTrue($"Application should depend only on Domain but {result.GetOffendingTypes()} does not");
        }
    }
}
