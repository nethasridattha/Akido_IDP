using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class SignedDocument
{
    public string CorrelationId { get; set; }

    public DateTime CreatedDate { get; set; }

    public string DocType { get; set; }

    public string SignedData { get; set; }
}
