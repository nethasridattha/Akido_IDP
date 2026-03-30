using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class RedemptionProof
{
    public int Id { get; set; }

    public DateTime UploadedAt { get; set; }

    public uint? VideoData { get; set; }

    public int RegistrantId { get; set; }

    public virtual RegistrantDetail Registrant { get; set; }
}
