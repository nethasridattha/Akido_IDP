using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class SimulatedBoarderControl
{
    public int Id { get; set; }

    public int IdDocType { get; set; }

    public string IdDocNumber { get; set; }

    public string EmiratesIdNumber { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string DateOfBirth { get; set; }

    public string Gender { get; set; }

    public string PlaceOfBirth { get; set; }

    public string Nationality { get; set; }

    public string CountryCode { get; set; }

    public string MobNo { get; set; }

    public string Email { get; set; }

    public string FatherName { get; set; }

    public string MotherName { get; set; }

    public string SpouseName { get; set; }

    public string Address { get; set; }

    public string SelfieImage { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string IdDocExpiryDate { get; set; }

    public string IdDocIssueDate { get; set; }

    public int Uin { get; set; }

    public string FamilyBook { get; set; }

    public string Occupation { get; set; }

    public string BloodGroup { get; set; }

    public string PersonNo { get; set; }

    public string FingerData { get; set; }
}
