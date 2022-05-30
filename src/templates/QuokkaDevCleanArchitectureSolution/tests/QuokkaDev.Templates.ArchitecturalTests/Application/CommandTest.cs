using FluentAssertions;
using QuokkaDev.Templates.Application.DI;
using NetArchTest.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace QuokkaDev.Templates.ArchitecturalTests.Application
{
    public class CommandTest
    {
        private const string COMMANDS_NAMESPACE = "QuokkaDev.Templates.Application.UseCases.[a-zA-z0-9]+.Commands";

        [Fact]
        public void Commands_Should_Reside_In_Namespace()
        {
            var result = Types.InAssembly(typeof(ServiceCollectionExtensions).Assembly)
                .That()
                .ResideInNamespaceMatching(COMMANDS_NAMESPACE)
                .Should()
                .HaveNameEndingWith("Command")
                .Or()
                .HaveNameEndingWith("CommandResult")
                .GetResult();

            result.IsSuccessful.Should().BeTrue($"queries should reside in namespaces matching {COMMANDS_NAMESPACE} and have name ending with 'Command' or 'CommandResult' but {result.GetOffendingTypes()} does not");
        }
    }
}
