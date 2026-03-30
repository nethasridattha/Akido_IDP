using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTPortal.Core.Enums
{
    public enum Status
    {
        [Display(Name = "Active")]
        [EnumMember(Value = "ACTIVE")]
        Active,

        [Display(Name = "Inactive")]
        [EnumMember(Value = "INACTIVE")]
        Inactive,

        [Display(Name = "Deleted")]
        [EnumMember(Value = "DELETED")]
        Deleted
    }
}
