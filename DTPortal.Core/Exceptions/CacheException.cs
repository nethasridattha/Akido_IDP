using System;

namespace DTPortal.Core.Exceptions
{
    public class CacheException : BaseException
    {

        public CacheException() : base()
        {
        }

        public CacheException(string message) : base(message)
        {
        }

        public CacheException(string message, Exception ex) :
            base(message, ex)
        {
        }

        public CacheException(string message, int statusCode) :
            base(message, statusCode)
        {
        }

        public CacheException(int errorCode, string message) :
            base(errorCode, message)
        {
        }
    }
}
