using DTPortal.Core.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class GetUserProfileResponse1
    {
        public bool success { get; set; }
        public string message { get; set; }
        public GetUserBasicProfileResult result { get; set; }

        public GetUserProfileResponse1(GetUserBasicProfileResult resource)
        {
            success = true;
            message = string.Empty;
            result = resource;
        }

        public GetUserProfileResponse1(GetUserBasicProfileResult resource, string Message)
        {
            success = true;
            message = Message;
            result = resource;
        }

        public GetUserProfileResponse1(string Message)
        {
            success = false;
            message = Message;
            result = default;
        }
    }
}
