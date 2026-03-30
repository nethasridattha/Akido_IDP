using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority
{
    public partial class GenericCategory
    {
        public GenericCategory()
        {
            GenericCategoryPrivileges = new HashSet<GenericCategoryPrivilege>();
            OrgCategoryPrivileges = new HashSet<OrgCategoryPrivilege>();
        }

        public int Id { get; set; }
        public string Category { get; set; }
        public string CategoryDisplayName { get; set; }
        public string Stakeholder { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual ICollection<GenericCategoryPrivilege> GenericCategoryPrivileges { get; set; }
        public virtual ICollection<OrgCategoryPrivilege> OrgCategoryPrivileges { get; set; }
    }
}
