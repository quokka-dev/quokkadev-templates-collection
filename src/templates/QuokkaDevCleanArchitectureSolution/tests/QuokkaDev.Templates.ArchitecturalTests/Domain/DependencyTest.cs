using FluentAssertions;
using NetArchTest.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using QuokkaDev.Templates.Domain;

namespace QuokkaDev.Templates.ArchitecturalTests.Domain
{
    public class DependencyTest
    {
        [Fact]
        public void Domain_Should_Have_No_Dependency_On_Other_Projects()
        {
            var result = Types.InAssembly(typeof(DomainException).Assembly)
                .Should()
                .NotHaveDependencyOnAny(
                    "QuokkaDev.Templates.Application",
                    "QuokkaDev.Templates.DataAccess.Commands",
                    "QuokkaDev.Templates.DataAccess.Queries",
                    "QuokkaDev.Templates.Api"
                ).GetResult();

            result.IsSuccessful.Should().BeTrue($"Domain should not depend on other projects but {result.GetOffendingTypes()} does not");
        }
    }
}
