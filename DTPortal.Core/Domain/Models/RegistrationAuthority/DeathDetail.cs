using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class DeathDetail
{
    public int Id { get; set; }

    public string FullName { get; set; }

    public string Gender { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string Nationality { get; set; }

    public string Address { get; set; }

    public DateOnly? DateOfDeath { get; set; }

    public string MannerOfDeath { get; set; }

    public string PlaceOfDeath { get; set; }

    public string InformantDetails { get; set; }

    public string ProofOfDeath { get; set; }

    public string InformantIdDocument { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string IdDocNumber { get; set; }

    public string FamilyDetails { get; set; }
}
