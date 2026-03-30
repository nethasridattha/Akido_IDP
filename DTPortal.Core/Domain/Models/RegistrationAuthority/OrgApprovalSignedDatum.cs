using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class OrgApprovalSignedDatum
{
    public int Id { get; set; }

    public int? Formid { get; set; }

    public string OrginalData { get; set; }

    public string SignedData { get; set; }

    public string ApprovalStatus { get; set; }

    public string CreatedOn { get; set; }
}
