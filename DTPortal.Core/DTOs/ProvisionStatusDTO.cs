using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class ProvisionStatusDTO
    {
        public int Id {  get; set; }
        public string Suid { get; set; }
        public string CredentialId { get; set; }
        public string DocumentId { get; set; }
        public string Status { get; set; }
    }
}
