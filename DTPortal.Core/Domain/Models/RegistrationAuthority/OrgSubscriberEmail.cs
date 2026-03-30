using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class OrgSubscriberEmail
{
    public int OrgContactsId { get; set; }

    public string OrganizationUid { get; set; }

    public string EmployeeEmail { get; set; }

    public bool? IsEsealSignatory { get; set; }

    public bool? IsEsealPreparatory { get; set; }

    public bool? IsOrgSignatory { get; set; }

    public string Designation { get; set; }

    public string SignaturePhoto { get; set; }

    public bool? IsTemplate { get; set; }

    public bool? IsBulkSign { get; set; }

    public string UgpassEmail { get; set; }

    public string PassportNumber { get; set; }

    public string NationalIdNumber { get; set; }

    public string MobileNumber { get; set; }

    public bool? UgpassUserLinkApproved { get; set; }

    public string SubscriberUid { get; set; }

    public string Status { get; set; }

    public bool? IsDelegate { get; set; }

    public string ShortSignature { get; set; }

    public bool? IsDigitalForm { get; set; }

    public virtual OrganizationDetail OrganizationU { get; set; }
}
