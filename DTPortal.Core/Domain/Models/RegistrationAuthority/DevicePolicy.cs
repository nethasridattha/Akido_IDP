using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class DevicePolicy
{
    public int Id { get; set; }

    public int? DeviceChangePolicyHour { get; set; }
}
