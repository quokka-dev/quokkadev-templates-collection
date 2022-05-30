namespace QuokkaDev.Templates.Application.Infrastructure.Exceptions
{
    public class InvalidQueryRequestException : ApplicationException
    {
        public InvalidQueryRequestException() : base()
        {
        }

        public InvalidQueryRequestException(string? message) : base(message)
        {
        }

        public InvalidQueryRequestException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
