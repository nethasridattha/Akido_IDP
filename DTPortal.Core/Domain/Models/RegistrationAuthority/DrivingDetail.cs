using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class DrivingDetail
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string IdDocumentNumber { get; set; }

    public string IssueDate { get; set; }

    public string ExpiryDate { get; set; }

    public string DrivingLicenseNo { get; set; }

    public string CreatedOn { get; set; }

    public string UpdatedOn { get; set; }

    public string MdlDocument { get; set; }

    public string Gender { get; set; }

    public string Birthdate { get; set; }

    public string PhoneNumber { get; set; }

    public string Photo { get; set; }

    public string IssuingAuthority { get; set; }

    public string IssuingCountry { get; set; }

    public string InternationalPermit { get; set; }

    public string Email { get; set; }
}
