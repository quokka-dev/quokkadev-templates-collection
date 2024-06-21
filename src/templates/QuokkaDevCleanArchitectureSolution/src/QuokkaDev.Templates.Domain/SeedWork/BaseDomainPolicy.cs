using System.Text;

namespace QuokkaDev.Templates.Domain.SeedWork
{
    public abstract class BaseDomainPolicy : IDomainPolicy
    {
        public bool Evaluate()
        {
            return ProcessPolicy().IsPassed;
        }

        public void ThrowIfNotPassed()
        {
            PolicyEvaluationResult result = ProcessPolicy();
            if (result.ErrorDetail is not null)
            {
                throw new PolicyException(result.ErrorDetail, 0);
            }
        }

        protected abstract PolicyEvaluationResult ProcessPolicy();  
    }

    public sealed class PolicyEvaluationResult
    {
        public bool IsPassed { get; private set; }
        public PolicyError? ErrorDetail { get; private set; }

        private PolicyEvaluationResult(bool isPassed, PolicyError? errorDetail)
        {
            IsPassed = isPassed;
            ErrorDetail = errorDetail;
        }

        public static PolicyEvaluationResult PassedResult()
        {
            return new PolicyEvaluationResult(true, null);
        }

        public static PolicyEvaluationResult NotPassedResult(PolicyError errorDetail)
        {
            return new PolicyEvaluationResult(false, errorDetail);
        }
    }

    public abstract record PolicyError(IEnumerable<string> Errors)
    {
        public override string ToString()
        {
            StringBuilder sb = new();            
            foreach (var error in Errors)
            {
                sb.AppendLine(error);
            }
            
            return sb.ToString();
        }
    }

    public sealed record GenericPolicyError() : PolicyError([]) { };
}
