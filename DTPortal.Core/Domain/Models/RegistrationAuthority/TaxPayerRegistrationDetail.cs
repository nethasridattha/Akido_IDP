using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class TaxPayerRegistrationDetail
{
    public int Id { get; set; }

    public string TaxPayerName { get; set; }

    public string TaxPayerEmail { get; set; }

    public string Tin { get; set; }

    public string MobileNumber { get; set; }

    public string TypeOfUser { get; set; }

    public string PostalAddress { get; set; }

    public string Country { get; set; }

    public string LicenceNumber { get; set; }

    public string RegistrationStatus { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string CustomsAgent { get; set; }
}
