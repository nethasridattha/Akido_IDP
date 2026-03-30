using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class UrabDetail
{
    public int Id { get; set; }

    public int? OrgOnboardingFormId { get; set; }

    public string OrgUraEncryptedDetails { get; set; }

    public string OrgUraReportPdf { get; set; }

    public string CeoUraEncryptedDetails { get; set; }

    public string CeoUraReportPdf { get; set; }

    public string SpocUraEncryptedDetails { get; set; }

    public string SpocUraReportPdf { get; set; }

    public string AuditorUraEncryptedDetails { get; set; }

    public string AuditorUraReportPdf { get; set; }
}
