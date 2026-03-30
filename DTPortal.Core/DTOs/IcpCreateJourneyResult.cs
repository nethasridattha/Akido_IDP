using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class IcpCreateJourneyResult
    {
        [JsonProperty("journeyToken")]
        public string journeyToken { get; set; }

        [JsonProperty("expiresIn")]
        public string expiresIn { get; set; }

        [JsonProperty("expiresAt")]
        public long expiresAt { get; set; }
    }

}
