using System;
using System.Collections.Generic;

#nullable disable

namespace DTPortal.Core.Domain.Models.RegistrationAuthority
{
    public partial class OrgContactsEmail
    {
        public int Id { get; set; }
        public int OrgId { get; set; }
        public string ContactEmail { get; set; }

        public virtual Organisation Org { get; set; }
    }
}
