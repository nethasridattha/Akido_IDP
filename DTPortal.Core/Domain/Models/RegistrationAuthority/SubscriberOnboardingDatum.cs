using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class SubscriberOnboardingDatum
{
    public int SubscriberOnboardingDataId { get; set; }

    public string SubscriberUid { get; set; }

    public int? TemplateId { get; set; }

    public string OnboardingMethod { get; set; }

    public string SelfieUri { get; set; }

    public string SubscriberType { get; set; }

    public string LevelOfAssurance { get; set; }

    public string IdDocType { get; set; }

    public string IdDocNumber { get; set; }

    public string PrevIdDocNumber { get; set; }

    public string IdDocUri { get; set; }

    public string OnboardingDataFieldsJson { get; set; }

    public string CreatedDate { get; set; }

    public string DocumentsLocation { get; set; }

    public string IdDocCode { get; set; }

    public string Geolocation { get; set; }

    public string Gender { get; set; }

    public string OptionalData1 { get; set; }

    public string SelfieThumbnailUri { get; set; }

    public string Remarks { get; set; }

    public string DateOfExpiry { get; set; }

    public string NiraResponse { get; set; }

    public string VerifierProvidedPhoto { get; set; }

    public string VoiceUrl { get; set; }

    public virtual Subscriber SubscriberU { get; set; }
}
