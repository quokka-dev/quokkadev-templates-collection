namespace QuokkaDev.Templates.Domain
{
    [Serializable]
    public class DomainException : ApplicationException
    {
        public DomainException(int code) : base()
        {
            HResult = code;
        }

        public DomainException(string? message, int code) : base(message)
        {
            HResult = code;
        }

        public DomainException(string? message, int code, Exception? innerException) : base(message, innerException)
        {
            HResult = code;
        }
    }
}
