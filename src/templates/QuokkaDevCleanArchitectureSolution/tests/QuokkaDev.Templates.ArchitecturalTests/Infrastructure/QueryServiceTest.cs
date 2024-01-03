using FluentAssertions;
using NetArchTest.Rules;
using QuokkaDev.Templates.Application.Services;
using Xunit;

namespace QuokkaDev.Templates.ArchitecturalTests.Infrastructure
{
    public class QueryServiceTest
    {
        private const string QUERYSERVICE_NAMESPACE = "QuokkaDev.Templates.DataAccess.Queries.Services";

        [Fact]
        public void QueryServices_Should_Reside_In_Namespace()
        {
            var result = Types.InAssembly(typeof(QuokkaDev.Templates.DataAccess.Queries.DI.ServiceCollectionExtensions).Assembly)
                .That()
                .ImplementInterface(typeof(IQueryService))
                .Should()
                .ResideInNamespaceMatching(QUERYSERVICE_NAMESPACE)
                .GetResult();

            result.IsSuccessful.Should().BeTrue($"QueryServices should reside in namespaces {QUERYSERVICE_NAMESPACE} but {result.GetOffendingTypes()} does not");
        }

        [Fact]
        public void QueryServices_Should_Have_Right_Name()
        {
            var result = Types.InAssembly(typeof(QuokkaDev.Templates.DataAccess.Queries.DI.ServiceCollectionExtensions).Assembly)
                .That()
                .ImplementInterface(typeof(IQueryService))
                .Should()
                .HaveNameEndingWith("QueryService")
                .GetResult();

            result.IsSuccessful.Should().BeTrue($"QueryServices should have name ending with 'QueryService' but {result.GetOffendingTypes()} does not");
        }

        [Fact]
        public void Class_In_Namespace_Should_Be_QueryServices()
        {
            var result = Types.InAssembly(typeof(QuokkaDev.Templates.DataAccess.Queries.DI.ServiceCollectionExtensions).Assembly)
                .That()
                .ResideInNamespaceMatching(QUERYSERVICE_NAMESPACE)
                .Should()
                .ImplementInterface(typeof(IQueryService))
                .GetResult();

            result.IsSuccessful.Should().BeTrue($"Classes in namespaces {QUERYSERVICE_NAMESPACE} should inherit from IQueryService but {result.GetOffendingTypes()} does not");
        }

        [Fact]
        public void Class_In_Namespace_Should_Have_Right_Name()
        {
            var result = Types.InAssembly(typeof(QuokkaDev.Templates.DataAccess.Queries.DI.ServiceCollectionExtensions).Assembly)
                .That()
                .ResideInNamespaceMatching(QUERYSERVICE_NAMESPACE)
                .Should()
                .HaveNameEndingWith("QueryService")
                .GetResult();

            result.IsSuccessful.Should().BeTrue($"Classes in namespaces {QUERYSERVICE_NAMESPACE} should have name ending with 'QueryService' but {result.GetOffendingTypes()} does not");
        }
    }
}
