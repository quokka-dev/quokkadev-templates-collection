using System.Linq.Expressions;

namespace QuokkaDev.Templates.Domain.Samples.HelloWorldMessages
{
    /// <summary>
    /// Specification for search customers based on external code
    /// </summary>
    public sealed class HelloWorldMessageTestSpecification : Specification<HelloWorldMessage>
    {
        public HelloWorldMessageTestSpecification()
        {
        }

        public override Expression<Func<HelloWorldMessage, bool>> ToExpression()
        {
            return agg => true;
        }
    }
}
