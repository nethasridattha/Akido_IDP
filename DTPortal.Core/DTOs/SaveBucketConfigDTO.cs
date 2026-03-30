using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class SaveBucketConfigDTO
    {
        public string orgId { get; set; }
        public string orgName { get; set; }
        public string appId { get; set; }
        public string label { get; set; }
        public string bucketClosingMessage { get; set; }
        public string status { get; set; }
        public int additionalDs { get; set; }
        public int additionalEds { get; set; }
    }
}