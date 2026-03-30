using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class OnboardingStepDetail
{
    public int StepId { get; set; }

    public string StepName { get; set; }

    public string StepDescription { get; set; }

    public string Status { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }
}
