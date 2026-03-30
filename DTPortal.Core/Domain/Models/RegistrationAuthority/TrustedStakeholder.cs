using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class TrustedStakeholder
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string SpocUgpassEmail { get; set; }

    public string ReferenceId { get; set; }

    public string OrganizationUid { get; set; }

    public short? Status { get; set; }

    public DateTime? OnboardingTime { get; set; }

    public DateTime CreationTime { get; set; }

    public string StakeholderType { get; set; }

    public string ReferredBy { get; set; }
}
