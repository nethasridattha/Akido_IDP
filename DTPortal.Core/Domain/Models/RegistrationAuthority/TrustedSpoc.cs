using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class TrustedSpoc
{
    public int Id { get; set; }

    public string SpocName { get; set; }

    public string Suid { get; set; }

    public string SpocEmail { get; set; }

    public string Status { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }

    public DateTime? InvitedOn { get; set; }

    public string IdDocumentNo { get; set; }

    public string MobileNumber { get; set; }
}
