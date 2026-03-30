using System;
using System.Collections.Generic;

#nullable disable

namespace DTPortal.Core.Domain.Models
{
    public partial class EmailTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MailSubject { get; set; }
        public string MailTemplate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Hash { get; set; }
        public string State { get; set; }
        public string BlockedReason { get; set; }
    }
}
