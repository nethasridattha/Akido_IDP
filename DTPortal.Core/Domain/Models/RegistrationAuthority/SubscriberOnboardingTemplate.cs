using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class SubscriberOnboardingTemplate
{
    public int TemplateId { get; set; }

    public string TemplateName { get; set; }

    public string TemplateMethod { get; set; }

    public string PublishedStatus { get; set; }

    public string State { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpatedDate { get; set; }

    public string Remarks { get; set; }

    public string CreatedBy { get; set; }

    public string ApprovedBy { get; set; }

    public string UpdatedBy { get; set; }

    public virtual ICollection<MapMethodOnboardingStep> MapMethodOnboardingSteps { get; set; } = new List<MapMethodOnboardingStep>();
}
