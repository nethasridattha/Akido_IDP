using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class QrCredentialVerifiersListDTO
    {
        public string credentialName { get; set; }
        public string credentialId { get; set; }
        public string organizationId { get; set; }
        public List<QrAttributesDTO> attributes { get; set; }
    }
}
