using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models;

public partial class EConsentClient
{
    public int Id { get; set; }

    public int ClientId { get; set; }

    public string Scopes { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string CreatedBy { get; set; }

    public string UpdatedBy { get; set; }

    public string Status { get; set; }

    public string Purposes { get; set; }

    public virtual Client Client { get; set; }
}
