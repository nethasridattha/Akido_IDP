using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class AdmissionInSchool
{
    public int Id { get; set; }

    public string ApplicantName { get; set; }

    public string DocumentNumber { get; set; }

    public string NumberOfChildren { get; set; }

    public string JsonData { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }

    public string ChildrenData { get; set; }
}
