using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services.Communication
{
    public class SubscriberInfo
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Idn { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string FullnameEN { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string FullnameAR { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string FirstnameEN { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string FirstnameAR { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string LastnameEN { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string LastnameAR { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string NationalityEN { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string NationalityAR { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Gender { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string IdType { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string TitleEN { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string TitleAR { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? DateOfBirth { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ProfileType { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Suid { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Loa { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string UnifiedId { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string PassportNumber { get; set; }
    }
}
