using QuokkaDev.Templates.Domain.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuokkaDev.Templates.Domain.Exceptions
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
