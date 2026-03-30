using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models;

public partial class ClientsPurpose
{
    public int Id { get; set; }

    public string ClientId { get; set; }

    public string PurposesAllowed { get; set; }
}
