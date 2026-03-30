using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class WalletConsentListDTO
    {
        public int id { get; set; }

        public string suid { get; set; }

        public string credentialName { get; set; }

        public string applicationName { get; set; }

        public string consentData { get; set; }

        public string status { get; set; }

        public DateTime createdDate { get; set; }

        public DateTime? updatedDate { get; set; }
    }
}
