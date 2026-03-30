using System;

namespace DTPortal.Core.Exceptions
{
    public abstract class BaseException : Exception
    {
        protected int StatusCode { get; }
        public int ErrorCode = 0;
        public string ErrorMessage = string.Empty;

        public BaseException() : base()
        {
        }

        public BaseException(string message) : base(message)
        {
        }

        public BaseException(string message, Exception ex) : base(message, ex)
        {
        }

        public BaseException(string message, int statusCode) : this(message)
        {
            StatusCode = statusCode;
        }

        public BaseException(int errorCode, string errorMessage) : this(errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }
    }
}
