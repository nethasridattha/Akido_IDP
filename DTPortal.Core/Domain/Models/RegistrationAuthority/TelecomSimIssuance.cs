using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class TelecomSimIssuance
{
    public int Id { get; set; }

    public string FullName { get; set; }

    public string IdDocNumber { get; set; }

    public string MobileNumber { get; set; }

    public string JsonBody { get; set; }

    public int? SimNumber { get; set; }
}
