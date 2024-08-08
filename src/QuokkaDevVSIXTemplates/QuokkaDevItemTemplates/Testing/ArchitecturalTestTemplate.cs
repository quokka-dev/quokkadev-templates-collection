using FluentAssertions;
using NetArchTest.Rules;
using QuokkaDev.Templates.Domain.SeedWork;
using Xunit;

namespace $rootnamespace$
{
    public class $safeitemname$
    {
        public $safeitemname$()
        {
        }

        [Fact(DisplayName = "Interfaces Should Have Right Name")]
        public void Interfaces_Should_Have_Right_Name()
        {
            var result = Types.InAssembly(typeof(IAggregateRoot).Assembly)
                .That()
                .AreInterfaces()
                .Should()
                .HaveNameStartingWith("I")
                .GetResult();

            result.IsSuccessful.Should().BeTrue($"Interfaces name should be start with 'I' but {result.GetOffendingTypes()} does not");
        }
    }
}
