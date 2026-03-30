using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class OrganizationStatus
{
    public int OrganizationStatusId { get; set; }

    public DateOnly? CreatedDate { get; set; }

    public string OtpVerifiedStatus { get; set; }

    public string OrganizationStatus1 { get; set; }

    public string OrganizationStatusDescription { get; set; }

    public string OrganizationUid { get; set; }

    public DateOnly? UpdatedDate { get; set; }

    public virtual OrganizationDetail OrganizationU { get; set; }
}
