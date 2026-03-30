using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class OrgSubscriberEmailOld
{
    public int OrgContactsId { get; set; }

    public string OrganizationUid { get; set; }

    public string SubEmailList { get; set; }

    public short? IsEsealSignatory { get; set; }

    public short? IsEsealPreparatory { get; set; }

    public short? IsOrgSignatory { get; set; }

    public string Designation { get; set; }

    public string SignaturePhoto { get; set; }

    public virtual OrganizationDetail OrganizationU { get; set; }
}
