using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class Hospitality
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string RoomAllocated { get; set; }

    public string DocumentNumber { get; set; }

    public string DateOfBirth { get; set; }

    public string AgeInYears { get; set; }

    public string Gender { get; set; }

    public string Photo { get; set; }

    public string CreationDate { get; set; }

    public string AgeOver18 { get; set; }

    public string JsonData { get; set; }
}
