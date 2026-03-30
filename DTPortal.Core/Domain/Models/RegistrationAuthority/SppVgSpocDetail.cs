using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class SppVgSpocDetail
{
    public long Id { get; set; }

    public string CreatedOn { get; set; }

    public long? OrgDetailsId { get; set; }

    public string SpocDocumentNumber { get; set; }

    public string SpocName { get; set; }

    public string SpocOfficialEmail { get; set; }

    public string UpdatedOn { get; set; }
}
