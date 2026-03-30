using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services.Communication
{
    public class ProfileConfig
    {
        public string UserStatusUrl { get; set; }
        public string ProfileUrl { get; set; }
    }

    public class ScopeInfo
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool Mandatory { get; set; }
        public bool ClaimsPresent { get; set; }
        public List<ClaimInfo> ClaimsInfo { get; set; }
    }

    public class ClaimInfo
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool Mandatory { get; set; }
    }
}
