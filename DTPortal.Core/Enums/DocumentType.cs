using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTPortal.Core.Enums
{
    public enum DocumentType
    {
        [Display(Name = "Pades")]
        [EnumMember(Value = "PADES")]
        Pades,

        [Display(Name = "Xades")]
        [EnumMember(Value = "XADES")]
        Xades,

        [Display(Name = "Cades")]
        [EnumMember(Value = "CADES")]
        Cades
    }
}
