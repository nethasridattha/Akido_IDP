using DTPortal.Core.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class UserProfilesConsentDTO
    {
        public string Suid { get; set; }

        public string ClientId { get; set; }
        public string ClientName { get; set; }

        public string Profile { get; set; }

        public List<string> Attributes { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string Status { get; set; }
    }
}
