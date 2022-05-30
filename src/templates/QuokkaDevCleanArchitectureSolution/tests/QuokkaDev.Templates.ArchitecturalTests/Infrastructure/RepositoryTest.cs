using FluentAssertions;
using QuokkaDev.Templates.DataAccess.Commands.DI;
using QuokkaDev.Templates.Domain.Interfaces;
using NetArchTest.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace QuokkaDev.Templates.ArchitecturalTests.Infrastructure
{
    public class RepositoryTest
    {
        private const string REPOSITORY_NAMESPACE = "QuokkaDev.Templates.DataAccess.Commands.Repositories";

        [Fact]
        public void Repositories_Should_Reside_In_Namespace()
        {
            var result = Types.InAssembly(typeof(DataAccessCommandsModule).Assembly)
                .That()
                .ImplementInterface(typeof(IRepository<>))
                .Should()
                .ResideInNamespaceMatching(REPOSITORY_NAMESPACE)
                .GetResult();

            result.IsSuccessful.Should().BeTrue($"Repositories should reside in namespaces {REPOSITORY_NAMESPACE} but {result.GetOffendingTypes()} does not");
        }

        [Fact]
        public void Repositories_Should_Have_Right_Name()
        {
            var result = Types.InAssembly(typeof(DataAccessCommandsModule).Assembly)
                .That()
                .ImplementInterface(typeof(IRepository<>))
                .Should()
                .HaveNameEndingWith("Repository")
                .Or()
                .HaveNameEndingWith("Repository`1")
                .GetResult();

            result.IsSuccessful.Should().BeTrue($"Repositories should have name ending with 'Repository' but {result.GetOffendingTypes()} does not");
        }

        [Fact]
        public void Class_In_Namespace_Should_Be_Repositories()
        {
            var result = Types.InAssembly(typeof(DataAccessCommandsModule).Assembly)
                .That()
                .ResideInNamespaceMatching(REPOSITORY_NAMESPACE)
                .Should()
                .ImplementInterface(typeof(IRepository<>))
                .GetResult();

            result.IsSuccessful.Should().BeTrue($"Classes in namespaces {REPOSITORY_NAMESPACE} should inherit from IRepository<> but {result.GetOffendingTypes()} does not");
        }

        [Fact]
        public void Class_In_Namespace_Should_Have_Right_Name()
        {
            var result = Types.InAssembly(typeof(DataAccessCommandsModule).Assembly)
                .That()
                .ResideInNamespaceMatching(REPOSITORY_NAMESPACE)
                .Should()
                .HaveNameEndingWith("Repository")
                .Or()
                .HaveNameEndingWith("Repository`1")
                .GetResult();

            result.IsSuccessful.Should().BeTrue($"Classes in namespaces {REPOSITORY_NAMESPACE} should have name ending with 'Repository' but {result.GetOffendingTypes()} does not");
        }

    }
}
