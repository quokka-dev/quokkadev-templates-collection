using FluentAssertions;
using QuokkaDev.Templates.Api;
using NetArchTest.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace QuokkaDev.Templates.ArchitecturalTests.Presentation
{
    public class DependencyTest
    {
        [Fact]
        public void Domain_Should_Have_No_Dependency_On_Other_Projects()
        {
            var result = Types.InAssembly(typeof(Startup).Assembly)
                .Should()
                .NotHaveDependencyOnAny(
                    "QuokkaDev.Templates.DataAccess.Commands",
                    "QuokkaDev.Templates.DataAccess.Queries"
                ).GetResult();

            result.IsSuccessful.Should().BeTrue($"Api should depend only on Domain and Application but {result.GetOffendingTypes()} does not");
        }
    }
}
