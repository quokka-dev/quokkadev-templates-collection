using NetArchTest.Rules;
using System;
using Xunit;
using FluentAssertions;
using QuokkaDev.Templates.Domain;

namespace QuokkaDev.Templates.ArchitecturalTests.Domain;

public class DomainExceptionsTest
{
    private const string EXCEPTION_NAMESPACE = "QuokkaDev.Templates.Domain.Exceptions";

    [Fact]
    public void Class_In_Domain_Exception_Namespasce_Should_Inherit_ApplicationException()
    {
        var result = Types.InAssembly(typeof(DomainException).Assembly)
            .That()
            .ResideInNamespace(EXCEPTION_NAMESPACE)
            .Should()
            .Inherit(typeof(ApplicationException))
            .GetResult();

        result.IsSuccessful.Should().BeTrue($"domain exceptions cannot inherit directly from Exception but {result.GetOffendingTypes()} does not");
    }

    [Fact]
    public void Exceptions_Should_Reside_In_Namespace()
    {
        var result = Types.InAssembly(typeof(DomainException).Assembly)
            .That()
            .Inherit(typeof(Exception))
            .Should()
            .ResideInNamespace(EXCEPTION_NAMESPACE)
            .GetResult();

        result.IsSuccessful.Should().BeTrue($"exceptions must reside in namespace {EXCEPTION_NAMESPACE} but {result.GetOffendingTypes()} does not");
    }
}