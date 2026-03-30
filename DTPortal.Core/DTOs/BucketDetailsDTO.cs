using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class OrgBucketConfig
    {
        public int id { get; set; }
        public string orgId { get; set; }
        public string orgName { get; set; }
        public string appId { get; set; }
        public string label { get; set; }
        public string bucketClosingMessage { get; set; }
        public string createdOn { get; set; }
        public string updatedOn { get; set; }
        public string status { get; set; }
        public int additionalDs { get; set; }
        public int additionalEds { get; set; }
    }

    public class BucketDetailsDTO
    {
        public int id { get; set; }
        public string bucketId { get; set; }
        public OrgBucketConfig orgBucketConfig { get; set; }
        public int totalDS { get; set; }
        public int totalEDS { get; set; }
        public string createdOn { get; set; }
        public string updatedOn { get; set; }
        public string status { get; set; }
        public string closedBy { get; set; }
        public string sponsorId { get; set; }
        public string closedOn { get; set; }
        [JsonProperty("paymentRecieved")]
        public bool paymentReceived { get; set; }
        public int remainingDSAfterPayment { get; set; }
        public int remainingEDSAfterPayment { get; set; }
    }
}
