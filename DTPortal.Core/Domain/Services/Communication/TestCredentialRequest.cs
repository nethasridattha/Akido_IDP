using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services.Communication
{
    public class TestCredentialRequest
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string UserId { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string CredentialId { get; set; }
    }
    public class QrTestCredentialRequest
    {
        public string CredentialId { get; set; }
        public Dictionary<string, JsonElement> Data { get; set; }
    }
}
