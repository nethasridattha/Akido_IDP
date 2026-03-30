using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class RegisterKycDeviceDTO
    {
        public int Id { get; set; }

        public string OrganizationId { get; set; }

        public string ClientId { get; set; }

        public string DeviceId { get; set; }

        public string Status { get; set; }
    }
}
