using System;
using System.Collections.Generic;

#nullable disable

namespace DTPortal.Core.Domain.Models.RegistrationAuthority
{
    public partial class Organisation
    {
        public Organisation()
        {
            OrgContactsEmails = new HashSet<OrgContactsEmail>();
        }

        public int OrgId { get; set; }
        public string OrgName { get; set; }
        public string OrgEsealUrl { get; set; }
        public string OrgDocDetailUrl { get; set; }

        public virtual ICollection<OrgContactsEmail> OrgContactsEmails { get; set; }
    }
}
