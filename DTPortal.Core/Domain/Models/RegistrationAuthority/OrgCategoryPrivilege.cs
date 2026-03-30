using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority
{
    public partial class OrgCategoryPrivilege
    {
        public int Id { get; set; }
        public string OrganizationUid { get; set; }
        public int? CategoryId { get; set; }
        public int? PrivlegeId { get; set; }
        public sbyte? PrivlegeStatus { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual GenericCategory Category { get; set; }
        public virtual GenericCategoryPrivilege Privlege { get; set; }
    }
}
