using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class SubscriberUgpassIdCard
{
    public int Id { get; set; }

    public string FullName { get; set; }

    public string DateOfBirth { get; set; }

    public string Gender { get; set; }

    public string Nationality { get; set; }

    public string CountryCode { get; set; }

    public string MobNo { get; set; }

    public string Email { get; set; }

    public string Address { get; set; }

    public string Photo { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string IdDocNumber { get; set; }

    public string PidIssueDate { get; set; }

    public string PidExpiryDate { get; set; }

    public string PidDocument { get; set; }

    public string CardNumber { get; set; }

    public string SubscriberUid { get; set; }
}
