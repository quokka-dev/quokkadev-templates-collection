using FluentAssertions;
using QuokkaDev.Cqrs.Abstractions;
using QuokkaDev.Templates.Application.DI;
using NetArchTest.Rules;
using Xunit;

namespace QuokkaDev.Templates.ArchitecturalTests.Application
{
    public class HandlerTest
    {
        private const string HANDLER_NAMESPACE = "QuokkaDev.Templates.Application.UseCases.[a-zA-Z0-9]+.Handlers";

        [Fact]
        public void Handlers_Should_Reside_In_Namespace()
        {
            var result = Types.InAssembly(typeof(ServiceCollectionExtensions).Assembly)
                .That()
                .ImplementInterface(typeof(IQueryHandler<,>))
                .Or()
                .ImplementInterface(typeof(ICommandHandler<,>))
                .Should()
                .HaveNameEndingWith("Handler")
                .And()
                .ResideInNamespaceMatching(HANDLER_NAMESPACE)
                .GetResult();

            result.IsSuccessful.Should().BeTrue($"Handlers should reside in namespaces matching {HANDLER_NAMESPACE} and have name ending with 'Handler' but {result.GetOffendingTypes()} does not");
        }

        [Fact]
        public void Class_In_Namespace_Should_Be_Handlers()
        {
            var result = Types.InAssembly(typeof(ServiceCollectionExtensions).Assembly)
                .That()
                .ResideInNamespaceMatching(HANDLER_NAMESPACE)
                .Should()
                .ImplementInterface(typeof(IQueryHandler<,>))
                .Or()
                .ImplementInterface(typeof(ICommandHandler<,>))
                .GetResult();

            result.IsSuccessful.Should().BeTrue($"Classes in namespaces matching {HANDLER_NAMESPACE} should be IQueryHandler or ICommandHadler but {result.GetOffendingTypes()} does not");
        }
    }
}
