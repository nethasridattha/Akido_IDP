using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTPortal.Core.Enums
{
    public enum OperationPriority
    {
        [EnumMember(Value = "HIGH")]
        HIGH,

        [EnumMember(Value = "LOW")]
        LOW,
    }
}
