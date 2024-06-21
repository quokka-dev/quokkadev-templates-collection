using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Filters;
using NetArchTest.Rules;
using Xunit;

namespace QuokkaDev.Templates.ArchitecturalTests.Presentation
{
    public class FilterTest
    {
        private const string FILTER_NAMESPACE = "QuokkaDev.Templates.Api.Filters";

        [Fact]
        public void Filters_Should_Reside_In_Namespace()
        {
            var result = Types.InAssembly(typeof(Program).Assembly)
                .That()
                .ImplementInterface(typeof(IAsyncExceptionFilter))
                .Or()
                .ImplementInterface(typeof(IAsyncActionFilter))
                .Or()
                .ImplementInterface(typeof(IAsyncResultFilter))
                .Or()
                .ImplementInterface(typeof(IAsyncAuthorizationFilter))
                .Or()
                .ImplementInterface(typeof(IAsyncResourceFilter))
                .Or()
                .ImplementInterface(typeof(IExceptionFilter))
                .Or()
                .ImplementInterface(typeof(IActionFilter))
                .Or()
                .ImplementInterface(typeof(IResultFilter))
                .Or()
                .ImplementInterface(typeof(IAuthorizationFilter))
                .Or()
                .ImplementInterface(typeof(IResourceFilter))
                .Should()
                .ResideInNamespaceMatching(FILTER_NAMESPACE)
                .GetResult();

            result.IsSuccessful.Should().BeTrue($"Filters should reside in namespaces {FILTER_NAMESPACE} but {result.GetOffendingTypes()} does not");
        }

        [Fact]
        public void Filters_Should_Have_Right_Name()
        {
            var result = Types.InAssembly(typeof(Program).Assembly)
                .That()
                .ImplementInterface(typeof(IAsyncExceptionFilter))
                .Or()
                .ImplementInterface(typeof(IAsyncActionFilter))
                .Or()
                .ImplementInterface(typeof(IAsyncResultFilter))
                .Or()
                .ImplementInterface(typeof(IAsyncAuthorizationFilter))
                .Or()
                .ImplementInterface(typeof(IAsyncResourceFilter))
                .Or()
                .ImplementInterface(typeof(IExceptionFilter))
                .Or()
                .ImplementInterface(typeof(IActionFilter))
                .Or()
                .ImplementInterface(typeof(IResultFilter))
                .Or()
                .ImplementInterface(typeof(IAuthorizationFilter))
                .Or()
                .ImplementInterface(typeof(IResourceFilter))
                .Should()
                .HaveNameEndingWith("Filter")
                .GetResult();

            result.IsSuccessful.Should().BeTrue($"Filters should have name ending with 'Filter' but {result.GetOffendingTypes()} does not");
        }

        [Fact]
        public void Class_In_Namespace_Should_Be_Controllers()
        {
            var result = Types.InAssembly(typeof(Program).Assembly)
                .That()
                .ResideInNamespaceMatching(FILTER_NAMESPACE)
                .Should()
                .ImplementInterface(typeof(IAsyncExceptionFilter))
                .Or()
                .ImplementInterface(typeof(IAsyncActionFilter))
                .Or()
                .ImplementInterface(typeof(IAsyncResultFilter))
                .Or()
                .ImplementInterface(typeof(IAsyncAuthorizationFilter))
                .Or()
                .ImplementInterface(typeof(IAsyncResourceFilter))
                .Or()
                .ImplementInterface(typeof(IExceptionFilter))
                .Or()
                .ImplementInterface(typeof(IActionFilter))
                .Or()
                .ImplementInterface(typeof(IResultFilter))
                .Or()
                .ImplementInterface(typeof(IAuthorizationFilter))
                .Or()
                .ImplementInterface(typeof(IResourceFilter))
                .GetResult();

            result.IsSuccessful.Should().BeTrue($"classes in namespaces {FILTER_NAMESPACE} should inherit from filter Interfaces but {result.GetOffendingTypes()} does not");
        }

        [Fact]
        public void Class_In_Namespace_Should_Have_Right_Name()
        {
            var result = Types.InAssembly(typeof(Program).Assembly)
                .That()
                .ResideInNamespaceMatching(FILTER_NAMESPACE)
                .Should()
                .HaveNameEndingWith("Filter")
                .GetResult();

            result.IsSuccessful.Should().BeTrue($"classes in namespaces {FILTER_NAMESPACE} should have name ending with 'Filter' but {result.GetOffendingTypes()} does not");
        }
    }
}
