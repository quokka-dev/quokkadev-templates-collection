using FluentAssertions;
using NetArchTest.Rules;
using QuokkaDev.Templates.Application.DI;
using Xunit;

namespace QuokkaDev.Templates.ArchitecturalTests.Application
{
    public class QueryTest
    {
        private const string QUERY_NAMESPACE = "QuokkaDev.Templates.Application.UseCases.[a-zA-z0-9]+.Queries";

        [Fact]
        public void Queries_Should_Reside_In_Namespace()
        {
            var result = Types.InAssembly(typeof(ServiceCollectionExtensions).Assembly)
                .That()
                .ResideInNamespaceMatching(QUERY_NAMESPACE)
                .Should()
                .HaveNameEndingWith("Query")
                .Or()
                .HaveNameEndingWith("QueryResult")
                .GetResult();

            result.IsSuccessful.Should().BeTrue($"queries should reside in namespaces matching {QUERY_NAMESPACE} and have name ending with 'Query' or 'QueryResult' but {result.GetOffendingTypes()} does not");
        }
    }
}
