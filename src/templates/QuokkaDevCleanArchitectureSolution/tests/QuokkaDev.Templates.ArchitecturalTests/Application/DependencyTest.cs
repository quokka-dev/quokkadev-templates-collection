using FluentAssertions;
using NetArchTest.Rules;
using QuokkaDev.Cqrs.Abstractions;
using QuokkaDev.Templates.Application;
using QuokkaDev.Templates.Application.Infrastructure.Interfaces;
using QuokkaDev.Templates.Application.Samples.Greetings;
using System;
using Xunit;

namespace QuokkaDev.Templates.ArchitecturalTests.Application
{
    public class DependencyTest
    {
        [Fact(DisplayName = "Application Should Have No Dependency On Other Projects But Domain")]
        public void Application_Should_Have_No_Dependency_On_Other_Projects_But_Domain()
        {
            var result = Types.InAssembly(typeof(ServiceCollectionExtensions).Assembly)
                .Should()
                .NotHaveDependencyOnAny(
                    "QuokkaDev.Templates.Persistence.Ef",
                    "QuokkaDev.Templates.Query.Dapper",
                    "QuokkaDev.Templates.Api"
                ).GetResult(); 

            result.IsSuccessful.Should().BeTrue($"Application should depend only on Domain but {result.GetOffendingTypes()} does not");
        }

        [Fact(DisplayName = "Commands Should Have No Dependency On ITransactionManager")]
        public void Commands_Should_Have_No_Dependency_On_ITransactionManager()
        {
            var result = Types.InAssembly(typeof(ServiceCollectionExtensions).Assembly) 
                .That()
                .ImplementInterface(typeof(ICommandHandler<,>))
                .Should()                
                .NotHaveDependencyOnAny(                    
                    typeof(IBatchCoreServices<>).FullName,
                    typeof(ITransactionManager).FullName,
                    typeof(ISpannedTransaction).FullName,
                    typeof(ISpannedTransactionBuilder).FullName,
                    typeof(ISpannedTransactionBuilderFactory).FullName                    
                ).GetResult();

            result.IsSuccessful.Should().BeTrue($"Commands should not use transactions but {result.GetOffendingTypes()} does not");
        }

        [Fact(DisplayName = "Batches Should Have No Dependency On ICurrentuserAccessor")]
        public void Batches_Should_Have_No_Dependency_On_ICurrentuserAccessor()
        {
            var result = Types.InAssembly(typeof(ServiceCollectionExtensions).Assembly)
                .That()
                .ImplementInterface(typeof(IBatch<>))
                .Should()
                .NotHaveDependencyOnAny(
                    typeof(ICommandsCoreServices<>).FullName,
                    typeof(ICurrentUserAccessor).FullName                    
                ).GetResult();

            result.IsSuccessful.Should().BeTrue($"Batch should not use current user but {result.GetOffendingTypes()} does not");
        }
    }
}
