using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority
{
    public partial class OrganizationService
    {
        public int OrganizationServiceId { get; set; }
        public string OrganizationUid { get; set; }
        public string ServiceName { get; set; }

        public virtual OrganizationDetail OrganizationU { get; set; }
    }
}
