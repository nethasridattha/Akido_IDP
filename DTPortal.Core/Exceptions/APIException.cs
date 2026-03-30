using System;

namespace DTPortal.Core.Exceptions
{
    public class APIException : BaseException
    {
        public APIException() : base()
        {
        }

        public APIException(string message) : base(message)
        {
        }

        public APIException(string message, Exception ex) : base(message, ex)
        {
        }

        public APIException(string message, int statusCode) : base(message, statusCode)
        {
        }
    }
}
