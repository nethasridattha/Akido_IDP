using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class BirthDetail
{
    public int Id { get; set; }

    public string FullName { get; set; }

    public string Gender { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string InformantDetails { get; set; }

    public string PlaceOfDelivery { get; set; }

    public string AttendantOfBirth { get; set; }

    public string TypeOfBirth { get; set; }

    public double? WeightInKgs { get; set; }

    public string FamilyDetails { get; set; }

    public string BirthProofCertificate { get; set; }

    public string InformantIddocument { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
