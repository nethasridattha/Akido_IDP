using System;

namespace DTPortal.Core.Exceptions
{
    public class ServiceNotAvailableException : BaseException
    {
        public ServiceNotAvailableException() : base()
        {
        }

        public ServiceNotAvailableException(string message) : base(message)
        {
        }

        public ServiceNotAvailableException(string message, Exception ex) : base(message, ex)
        {
        }

        public ServiceNotAvailableException(string message, int statusCode) : base(message, statusCode)
        {
        }
    }
}
