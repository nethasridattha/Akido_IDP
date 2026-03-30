using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class AttributeServiceTransactionsDTO
    {
        public AttributeProfileRequest attributeProfileRequest { get; set; } = new AttributeProfileRequest();
        public AttributeProfileConsent attributeProfileConsent { get; set; }=new AttributeProfileConsent();
        public AttributeProfileStatus attributeProfileStatus { get; set; } = new AttributeProfileStatus();

    }
    public class AttributeProfileRequest
    {
        public int Id { get; set; }
        public string TransactionId { get; set; }
        public string ClientName { get; set; }
        public DateTime RequestDate { get; set; }
        public string RequestProfile { get; set; }
        public string UserId { get; set; }
        public string RequestPurpose { get; set; }
    }
    public class AttributeProfileConsent
    {
        public string ConsentStatus { get; set; }
        public string ApprovedProfileAttributes { get; set; }
        public string RequestedProfileAttributes { get; set; }
        public DateTime ConsentUpdatedDate { get; set; }
    }
    public class AttributeProfileStatus
    {
        public string Status { get; set; }
        public string FailedReason { get; set; }

        //public int DataPivotId { get; set; }
    }
}
