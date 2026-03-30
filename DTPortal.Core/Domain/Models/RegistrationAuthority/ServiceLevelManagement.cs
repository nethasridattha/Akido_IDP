using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class ServiceLevelManagement
{
    public int Id { get; set; }

    public int Loa { get; set; }

    public string ServiceName { get; set; }
}
