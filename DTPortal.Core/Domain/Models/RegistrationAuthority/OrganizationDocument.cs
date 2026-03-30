using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class OrganizationDocument
{
    public int OrganizationDocumentsId { get; set; }

    public string OrganizationUid { get; set; }

    public string ESealImage { get; set; }

    public string OrgDetailsPdf { get; set; }

    public string IncorporationFile { get; set; }

    public string TaxFile { get; set; }

    public string AnyOtherDoc { get; set; }

    public virtual OrganizationDetail OrganizationU { get; set; }
}
