using System;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Models.RegistrationAuthority;

public partial class VoiceEnrollment
{
    public int Id { get; set; }

    public string Email { get; set; }

    public byte[] VoiceRecordingFile { get; set; }

    public string Suid { get; set; }
}
