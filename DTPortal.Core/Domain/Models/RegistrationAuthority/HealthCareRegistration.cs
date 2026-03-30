using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class HealthCareRegistration
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

    public string CreatedOn { get; set; }

    public string UpdatedOn { get; set; }

    public string PolicyDocument { get; set; }
}
