using System;

namespace DTPortal.Core.Exceptions
{
    public class GatewayTimeoutException : BaseException
    {
        public GatewayTimeoutException() : base()
        {
        }

        public GatewayTimeoutException(string message) : base(message)
        {
        }

        public GatewayTimeoutException(string message, Exception ex) : base(message, ex)
        {
        }

        public GatewayTimeoutException(string message, int statusCode) : base(message, statusCode)
        {
        }
    }
}
