using FluentAssertions;
using FluentValidation;
using NetArchTest.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace QuokkaDev.Templates.ArchitecturalTests.Application
{
    public class ValidatorTest
    {
        private const string VALIDATOR_NAMESPACE = "QuokkaDev.Templates.Application.UseCases.[a-zA-z0-9]+.Validators";

        [Fact]
        public void Validators_Should_Reside_In_Namespace()
        {
            var result = Types.InAssembly(typeof(ServiceCollectionExtensions).Assembly)
                .That()
                .Inherit(typeof(AbstractValidator<>))
                .Should()
                .ResideInNamespaceMatching(VALIDATOR_NAMESPACE)
                .GetResult();

            result.IsSuccessful.Should().BeTrue($"Validators should reside in namespaces matching {VALIDATOR_NAMESPACE} but {result.GetOffendingTypes()} does not");
        }

        [Fact]
        public void Validators_Should_Have_Right_Name()
        {
            var result = Types.InAssembly(typeof(ServiceCollectionExtensions).Assembly)
                .That()
                .Inherit(typeof(AbstractValidator<>))
                .Should()
                .HaveNameEndingWith("Validator")
                .GetResult();

            result.IsSuccessful.Should().BeTrue($"Validators should have name ending with 'Validator' but {result.GetOffendingTypes()} does not");
        }

        [Fact]
        public void Class_In_Namespace_Should_Be_Validators()
        {
            var result = Types.InAssembly(typeof(ServiceCollectionExtensions).Assembly)
                .That()
                .ResideInNamespaceMatching(VALIDATOR_NAMESPACE)
                .Should()
                .Inherit(typeof(AbstractValidator<>))
                .GetResult();

            result.IsSuccessful.Should().BeTrue($"Classes in namespaces matching {VALIDATOR_NAMESPACE} should inherit from AbstractValidator but {result.GetOffendingTypes()} does not");
        }

        [Fact]
        public void Class_In_Namespace_Should_Have_Right_Name()
        {
            var result = Types.InAssembly(typeof(ServiceCollectionExtensions).Assembly)
                .That()
                .ResideInNamespaceMatching(VALIDATOR_NAMESPACE)
                .Should()
                .HaveNameEndingWith("Validator")
                .GetResult();

            result.IsSuccessful.Should().BeTrue($"Classes in namespaces matching {VALIDATOR_NAMESPACE} should have name ending with 'Validator' but {result.GetOffendingTypes()} does not");
        }
    }
}
