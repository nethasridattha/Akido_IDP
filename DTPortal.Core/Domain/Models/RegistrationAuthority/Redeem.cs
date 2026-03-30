using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class Redeem
{
    public int Id { get; set; }

    public string Code { get; set; }

    public byte[] VideoFile { get; set; }

    public string UploadedOn { get; set; }
}
