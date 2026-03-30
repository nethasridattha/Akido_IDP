using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class QrCredentialListDTO
    {
        public string credentialName { get; set; }
        public string displayName { get; set; }

        public string credentialId { get; set; }

        public string documentName { get; set; }
        public string categoryName { get; set; }

        public string authenticationScheme { get; set; }

        public string categoryId { get; set; }

        public string logo { get; set; }

        public string organizationId { get; set; }

        public string status { get; set; }
        public bool portraitVerificationRequired { get; set; }
    }
}
