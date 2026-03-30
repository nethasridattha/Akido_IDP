using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class SubscriberCardDetail
{
    public int Id { get; set; }

    public string SubscriberUid { get; set; }

    public string IdDocNumber { get; set; }

    public string CardDocumnet { get; set; }
}
