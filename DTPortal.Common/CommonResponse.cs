using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Common
{
    public class CommonResponse
    {
        public class Response
        {
            public bool Success { get; set; }
            public string Message { get; set; }
            public string Result { get; set; }
        }

        public class StatusResponse
        {
            public bool status { get; set; }
            public string message { get; set; }
            public string result { get; set; }
        }


        public class ResponseStatus
        {
            public int result { get; set; }
            public ErrorResponse errorObj { get; set; }

        }

        public class ErrorResponse
        {
            public string error { get; set; }
            public string error_description { get; set; }
        }
    }
}
