using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class RegistrantDetail
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Address { get; set; }

    public string BirthPlace { get; set; }

    public string DateOfBirth { get; set; }

    public string Gender { get; set; }

    public string PhoneNumber { get; set; }

    public string IdDocNumber { get; set; }

    public string Occupation { get; set; }

    public double? Income { get; set; }

    public string MaritalStatus { get; set; }

    public string CreatedOn { get; set; }

    public string UpdatedOn { get; set; }

    public string Age { get; set; }

    public string Country { get; set; }

    public string State { get; set; }

    public string City { get; set; }

    public string Pincode { get; set; }

    public string BankAccountStatus { get; set; }

    public string AccountHolderName { get; set; }

    public string BankAccountNumber { get; set; }

    public string SwiftCode { get; set; }

    public string AccountType { get; set; }

    public string Suid { get; set; }

    public string Photo { get; set; }

    public virtual ICollection<CycleMembership> CycleMemberships { get; set; } = new List<CycleMembership>();

    public virtual ICollection<Entitlement> Entitlements { get; set; } = new List<Entitlement>();

    public virtual ICollection<PaymentTransaction> PaymentTransactions { get; set; } = new List<PaymentTransaction>();

    public virtual ICollection<ProgramMembership> ProgramMemberships { get; set; } = new List<ProgramMembership>();

    public virtual ICollection<RedemptionProof> RedemptionProofs { get; set; } = new List<RedemptionProof>();
}
