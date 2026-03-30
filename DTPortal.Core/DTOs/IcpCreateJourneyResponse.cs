using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class IcpCreateJourneyResponse
    {
        [JsonProperty("result")]
        public IcpCreateJourneyResult Success { get; set; }

        [JsonProperty("error")]
        public IcpError Error { get; set; }

        [JsonProperty("signature")]
        public string Signature { get; set; }
    }


}
