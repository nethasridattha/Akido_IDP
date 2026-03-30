using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class OrganizationUsageReport
{
    public int Id { get; set; }

    public string OrganizationId { get; set; }

    public string ReportMonth { get; set; }

    public string ReportYear { get; set; }

    public string PdfReport { get; set; }

    public double TotalInvoiceAmount { get; set; }

    public string CreatedOn { get; set; }
}
