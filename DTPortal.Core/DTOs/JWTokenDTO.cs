using DTPortal.Core.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class JWTokenDTO
    {
        public string iss { get; set; }
        public string sub { get; set; }
        public string aud { get; set; }
        public long exp { get; set; }
        public long iat { get; set; }
        public string nonce { get; set; }
        public long auth_time { get; set; }
        public string at_hash { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string loa { get; set; }
        public string daes_id_document_type { get; set; }
        public string daes_id_document_number { get; set; }
        public string gender { get; set; }
        public string birthdate { get; set; }
    }

    public class SubIdToken
    {
        public string iss { get; set; }
        public string sub { get; set; }
        public string aud { get; set; }
        public long exp { get; set; }
        public long iat { get; set; }
        public string nonce { get; set; }
        public long auth_time { get; set; }
        public string at_hash { get; set; }
        public SubscriberObject daes_claims { get; set; }
    }
}
