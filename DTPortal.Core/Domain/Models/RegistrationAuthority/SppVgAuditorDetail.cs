using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class SppVgAuditorDetail
{
    public long Id { get; set; }

    public string AuditorDocumentNumber { get; set; }

    public string AuditorName { get; set; }

    public string AuditorOfficialEmail { get; set; }

    public string CreatedOn { get; set; }

    public long? OrgDetailsId { get; set; }

    public string UpdatedOn { get; set; }
}
