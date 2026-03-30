using System;

namespace DTPortal.Core.Exceptions
{
    public class PKIException : BaseException
    {

        public PKIException() : base()
        {
        }

        public PKIException(string message) : base(message)
        {
        }

        public PKIException(string message, Exception ex) : base(message, ex)
        {
        }

        public PKIException(string message, int statusCode) : base(message, statusCode)
        {
        }
    }
}
