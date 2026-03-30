using System;

namespace DTPortal.Core.Exceptions
{
    public class RequestTimeoutException : BaseException
    {
        public RequestTimeoutException() : base()
        {
        }

        public RequestTimeoutException(string message) : base(message)
        {
        }

        public RequestTimeoutException(string message, Exception ex) : base(message, ex)
        {
        }

        public RequestTimeoutException(string message, int statusCode) : base(message, statusCode)
        {
        }
    }
}
