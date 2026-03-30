using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class OnboardingAgent
{
    public int Id { get; set; }

    public string AgentUgpassSuid { get; set; }

    public string AgentName { get; set; }

    public string AgentUgpassMobileNumber { get; set; }

    public string AgentUgpassEmail { get; set; }

    public string DeviceId { get; set; }

    public string FcmToken { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string Status { get; set; }
}
