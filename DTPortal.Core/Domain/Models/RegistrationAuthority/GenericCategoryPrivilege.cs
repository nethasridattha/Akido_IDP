using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority
{
    public partial class GenericCategoryPrivilege
    {
        public GenericCategoryPrivilege()
        {
            OrgCategoryPrivileges = new HashSet<OrgCategoryPrivilege>();
        }

        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public string Privilege { get; set; }
        public string PrivilegeDisplayName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual GenericCategory Category { get; set; }
        public virtual ICollection<OrgCategoryPrivilege> OrgCategoryPrivileges { get; set; }
    }
}
