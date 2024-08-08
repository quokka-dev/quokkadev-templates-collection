using Asp.Versioning;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NetArchTest.Rules;
using Xunit;

namespace QuokkaDev.Templates.ArchitecturalTests.Presentation
{
    public class ControllerTest
    {
        [Fact(DisplayName = "Controllers Should Have ApiControllerAttribute")]
        public void Controllers_Should_Have_ApiControllerAttribute()
        {
            var result = Types.InAssembly(typeof(Program).Assembly)
                .That()
                .Inherit(typeof(ControllerBase))
                .Should()
                .HaveCustomAttribute(typeof(ApiControllerAttribute))
                .GetResult();

            result.IsSuccessful.Should().BeTrue($"Controllers should have ApiControllerAttribute but {result.GetOffendingTypes()} does not");
        }

        [Fact(DisplayName = "Controllers Should Have RouteAttribute")]
        public void Controllers_Should_Have_RouteAttribute()
        {
            var result = Types.InAssembly(typeof(Program).Assembly)
                .That()
                .Inherit(typeof(ControllerBase))
                .Should()
                .HaveCustomAttribute(typeof(RouteAttribute))
                .GetResult();

            result.IsSuccessful.Should().BeTrue($"Controllers should have RouteAttribute but {result.GetOffendingTypes()} does not");
        }

        [Fact(DisplayName = "Controllers Should Have ApiVersionAttribute")]
        public void Controllers_Should_Have_ApiVersionAttribute()
        {
            var result = Types.InAssembly(typeof(Program).Assembly)
                .That()
                .Inherit(typeof(ControllerBase))
                .Should()
                .HaveCustomAttribute(typeof(ApiVersionAttribute))
                .GetResult();

            result.IsSuccessful.Should().BeTrue($"Controllers should have ApiVersionAttribute but {result.GetOffendingTypes()} does not");
        }
    }
}
