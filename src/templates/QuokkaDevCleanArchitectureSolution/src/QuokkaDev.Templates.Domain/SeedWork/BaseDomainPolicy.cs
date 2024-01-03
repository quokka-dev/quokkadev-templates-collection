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
            if (result.Exception is not null)
            {
                throw result.Exception;
            }
        }

        protected abstract PolicyEvaluationResult ProcessPolicy();

        protected sealed class PolicyEvaluationResult
        {
            public bool IsPassed { get; private set; }
            public DomainException? Exception { get; private set; }

            private PolicyEvaluationResult(bool isPassed, DomainException? exception)
            {
                IsPassed = isPassed;
                Exception = exception;
            }

            public static PolicyEvaluationResult PassedResult()
            {
                return new PolicyEvaluationResult(true, null);
            }

            public static PolicyEvaluationResult NotPassedResult(DomainException exception)
            {
                return new PolicyEvaluationResult(false, exception);
            }
        }
    }
}
