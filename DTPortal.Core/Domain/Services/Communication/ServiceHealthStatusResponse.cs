using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services.Communication
{
    public class ServiceResponse
    {
        public string ServiceName { get; set; }
        public bool Status { get; set; }
    }

    public class ServiceHealthStatusResponse
    {
        public ServiceHealthStatusResponse()
        {
            serviceResponses = new List<ServiceResponse>();
        }

        public IList<ServiceResponse> serviceResponses;
    }
}
