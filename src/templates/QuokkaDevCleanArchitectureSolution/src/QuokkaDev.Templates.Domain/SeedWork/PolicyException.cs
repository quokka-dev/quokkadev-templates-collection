using System.Runtime.Serialization;

namespace QuokkaDev.Templates.Domain.SeedWork
{    
    public class PolicyException : Exception
    {
        public PolicyError DomainError { get; }

        public PolicyException(PolicyError domainError, int code) : base(domainError.ToString())
        {
            HResult = code;
            DomainError = domainError;
        }

        public PolicyException(PolicyError domainError, int code, Exception? innerException) : base(domainError.ToString(), innerException)
        {
            HResult = code;
            DomainError = domainError;
        }        
    }
}
