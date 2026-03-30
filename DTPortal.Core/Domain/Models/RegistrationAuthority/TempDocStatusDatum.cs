using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class TempDocStatusDatum
{
    public int Id { get; set; }

    public string DocId { get; set; }

    public string Status { get; set; }

    public int PoaId { get; set; }

    public string Agent { get; set; }

    public string Notary { get; set; }
}
