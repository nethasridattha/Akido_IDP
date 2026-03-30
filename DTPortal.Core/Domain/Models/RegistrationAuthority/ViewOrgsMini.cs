using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority
{
    public partial class ViewOrgsMini
    {
        public int OrganizationDetailsId { get; set; }
        public string OrganizationUid { get; set; }
        public string OrgName { get; set; }
        public string OrganizationStatus { get; set; }
        public bool? EnablePostPaidOption { get; set; }
        public string SpocUgpassEmail { get; set; }
        public string AgentUrl { get; set; }
    }
}
