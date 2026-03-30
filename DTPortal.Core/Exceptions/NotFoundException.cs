using System;

namespace DTPortal.Core.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException() : base()
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, Exception ex) : base(message, ex)
        {
        }

        public NotFoundException(string message, int statusCode) : base(message, statusCode)
        {
        }
    }
}
