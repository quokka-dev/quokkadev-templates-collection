namespace QuokkaDev.Templates.Domain
{
    public class EntityNotFoundException : ApplicationException
    {
        public EntityNotFoundException() : base()
        {
            HResult = Constants.ErrorCodes.ENTITY_NOT_FOUND;
        }

        public EntityNotFoundException(string? message) : base(message)
        {
            HResult = Constants.ErrorCodes.ENTITY_NOT_FOUND;
        }

        public EntityNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
            HResult = Constants.ErrorCodes.ENTITY_NOT_FOUND;
        }
    }
}
