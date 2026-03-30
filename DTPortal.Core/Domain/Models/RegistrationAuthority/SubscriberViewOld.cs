using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority
{
    public partial class SubscriberViewOld
    {
        public string SubscriberUid { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string IdDocType { get; set; }
        public string IdDocNumber { get; set; }
        public string DisplayName { get; set; }
        public string CertificateStatus { get; set; }
        public string MobileNumber { get; set; }
        public string SubscriberStatus { get; set; }
        public string Email { get; set; }
        public string FcmToken { get; set; }
        public string Loa { get; set; }
        public string Gender { get; set; }
        public string Country { get; set; }
    }
}
