using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services.Communication
{
    public class FaceValidateResponse
    {
        public string Id_Number { get; set; }
        public bool Status { get; set; }
    }

    public class FaceValidateRequest
    {
        public string subscriberPhoto { get; set; }
    }

    public class FaceValidationRequest
    {
        public string base64Face { get; set; }
        public string ip {  get; set; }
        public string typeOfDevice { get; set; }
        public string userAgent { get; set; }
        public string clientId { get; set; }
    }
}
