using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class HospitalInsurancePolicy
{
    public int Id { get; set; }

    public string InsuredName { get; set; }

    public string Gender { get; set; }

    public string PhoneNumber { get; set; }

    public string PolicyNumber { get; set; }

    public string PolicyName { get; set; }

    public string Photo { get; set; }

    public string PolicyStartDate { get; set; }

    public string PolicyEndDate { get; set; }

    public string PolicyStatus { get; set; }

    public string JsonData { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string PolicyDocument { get; set; }
}
