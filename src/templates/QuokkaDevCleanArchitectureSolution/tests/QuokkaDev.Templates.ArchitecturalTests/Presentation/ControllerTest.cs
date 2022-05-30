using FluentAssertions;
using QuokkaDev.Templates.Api;
using Microsoft.AspNetCore.Mvc;
using NetArchTest.Rules;
using Xunit;

namespace QuokkaDev.Templates.ArchitecturalTests.Presentation
{
    public class ControllerTest
    {
        private const string CONTROLLER_NAMESPACE = "QuokkaDev.Templates.Api.Controllers";

        [Fact]
        public void Controllers_Should_Reside_In_Namespace()
        {
            var result = Types.InAssembly(typeof(Startup).Assembly)
                .That()
                .Inherit(typeof(ControllerBase))
                .Should()
                .ResideInNamespaceMatching(CONTROLLER_NAMESPACE)
                .GetResult();

            result.IsSuccessful.Should().BeTrue($"Controllers should reside in namespaces {CONTROLLER_NAMESPACE} but {result.GetOffendingTypes()} does not");
        }

        [Fact]
        public void Controllers_Should_Have_Right_Name()
        {
            var result = Types.InAssembly(typeof(Startup).Assembly)
                .That()
                .Inherit(typeof(ControllerBase))
                .Should()
                .HaveNameEndingWith("Controller")
                .GetResult();

            result.IsSuccessful.Should().BeTrue($"Controllers should have name ending with 'Controller' but {result.GetOffendingTypes()} does not");
        }

        [Fact]
        public void Class_In_Namespace_Should_Be_Controllers()
        {
            var result = Types.InAssembly(typeof(Startup).Assembly)
                .That()
                .ResideInNamespaceMatching(CONTROLLER_NAMESPACE)
                .And()
                .AreNotStatic()
                .Should()
                .Inherit(typeof(ControllerBase))
                .GetResult();

            result.IsSuccessful.Should().BeTrue($"non static classes in namespaces {CONTROLLER_NAMESPACE} should inherit from ControllerBase but {result.GetOffendingTypes()} does not");
        }

        [Fact]
        public void Class_In_Namespace_Should_Have_Right_Name()
        {
            var result = Types.InAssembly(typeof(Startup).Assembly)
                .That()
                .ResideInNamespaceMatching(CONTROLLER_NAMESPACE)
                .And()
                .AreNotStatic()
                .Should()
                .HaveNameEndingWith("Controller")
                .GetResult();

            result.IsSuccessful.Should().BeTrue($"non static classes in namespaces {CONTROLLER_NAMESPACE} should have name ending with 'Controller' but {result.GetOffendingTypes()} does not");
        }

        [Fact]
        public void Controllers_Should_Have_ApiControllerAttrbiute()
        {
            var result = Types.InAssembly(typeof(Startup).Assembly)
                .That()
                .Inherit(typeof(ControllerBase))
                .Should()
                .HaveCustomAttribute(typeof(ApiControllerAttribute))
                .GetResult();

            result.IsSuccessful.Should().BeTrue($"Controllers should have ApiControllerAttribute but {result.GetOffendingTypes()} does not");
        }

        [Fact]
        public void Controllers_Should_Have_RouteAttrbiute()
        {
            var result = Types.InAssembly(typeof(Startup).Assembly)
                .That()
                .Inherit(typeof(ControllerBase))
                .Should()
                .HaveCustomAttribute(typeof(RouteAttribute))
                .GetResult();

            result.IsSuccessful.Should().BeTrue($"Controllers should have RouteAttribute but {result.GetOffendingTypes()} does not");
        }

        [Fact]
        public void Controllers_Should_Have_ApiVersionAttrbiute()
        {
            var result = Types.InAssembly(typeof(Startup).Assembly)
                .That()
                .Inherit(typeof(ControllerBase))
                .Should()
                .HaveCustomAttribute(typeof(ApiVersionAttribute))
                .GetResult();

            result.IsSuccessful.Should().BeTrue($"Controllers should have ApiVersionAttribute but {result.GetOffendingTypes()} does not");
        }
    }
}
