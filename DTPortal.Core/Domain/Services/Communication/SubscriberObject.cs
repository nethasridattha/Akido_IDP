using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services.Communication
{
    public class SubscriberObject
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string idn { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string fullnameEN { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string fullnameAR { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string firstnameEN { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string firstnameAR { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string lastnameEN { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string lastnameAR { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string nationalityEN { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string nationalityAR { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string gender { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string idType { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string titleEN { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string titleAR { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? dateOfBirth { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string profileType { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string suid { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string loa { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string unifiedId { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string passportNumber { get; set; }
    }
}
