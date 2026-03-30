using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class HotelOfflineLog
{
    public int Id { get; set; }

    public string Type { get; set; }

    public string DocumentNumber { get; set; }

    public DateTime CreatedOn { get; set; }
}
