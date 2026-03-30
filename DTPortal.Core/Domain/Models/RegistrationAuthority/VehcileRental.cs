using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class VehcileRental
{
    public int Id { get; set; }

    public string ApplicantName { get; set; }

    public string PassportNumber { get; set; }

    public string PassportDocument { get; set; }

    public string PidDocument { get; set; }

    public string DrivingLicenseNumber { get; set; }

    public string DrivingLicenseDocument { get; set; }

    public string InternationalPermit { get; set; }

    public string CreatedOn { get; set; }

    public string UpdatedOn { get; set; }

    public string Status { get; set; }

    public string Country { get; set; }

    public string NoOfDays { get; set; }

    public string PickUpDate { get; set; }

    public string CarNumber { get; set; }

    public string JsonData { get; set; }

    public string RentalAgreementFile { get; set; }

    public string Photo { get; set; }
}
