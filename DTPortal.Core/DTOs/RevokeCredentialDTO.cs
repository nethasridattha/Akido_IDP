using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class RevokeCredentialDTO
    {
        public string issuerID {  get; set; }
        public string suid { get; set; }
        public string credentialType { get; set; }
    }
}
