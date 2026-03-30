using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class QrCredentialVerifierDTO
    {
        public int id { get; set; }
        public string credentialId { get; set; }
        public string organizationId { get; set; }
        public string credentialName { get; set; }
        public string organizationName { get; set; }
        public QrAttributesDTO attributes { get; set; }
        public List<string> emails { get; set; }
        public string status { get; set; }
        public DateTime? createdDate { get; set; }
        public DateTime? updatedDate { get; set; }
        public string remarks { get; set; }
    }
     
}
