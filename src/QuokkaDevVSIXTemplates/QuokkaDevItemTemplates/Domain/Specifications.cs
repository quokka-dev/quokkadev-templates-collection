using System.Linq.Expressions;

namespace $rootnamespace$
{
    /// <summary>
    /// Specification for search customers based on external code
    /// </summary>
    public sealed class $fileinputname$TestSpecification : Specification<$fileinputname$>
    {    
        public $fileinputname$TestSpecification()
        {            
        }

        public override Expression<Func<$fileinputname$, bool>> ToExpression()
        {
            return agg => true;
        }
    }
}
