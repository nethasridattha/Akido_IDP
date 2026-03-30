using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class TestCredentialDTO
    {
        public string issuerID {  get; set; }
        public string suid { get; set; }
        public string credentialId { get; set; }
        public string credentialType { get; set; }
        public Dictionary<string, Object> Data { get; set; }
        public bool qr { get; set; }
    }
}
