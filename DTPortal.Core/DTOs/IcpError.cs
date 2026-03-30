using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class IcpError
    {
        [JsonProperty("code")]
        public string code { get; set; }

        [JsonProperty("message")]
        public string message { get; set; }
    }

}
