using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class KycApplicationDTO
    {
        public int Id { get; set; }
        public string OrganizationId { get; set; }
        public string ApplicationName { get; set; }
        public string clientId { get; set; }
        public string clientSecret { get; set; }

        public string status { get; set; }
    }
}
