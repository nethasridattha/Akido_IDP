using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DTPortal.IDP.DTOs
{
    [JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Disallow)]
    public class VerifyTokenReq
    {
        [Required]
        [StringLength(200, MinimumLength = 1)]
        [RegularExpression(@"^(?!\s*$)[A-Za-z0-9\-\._~\+\/=]+$",
            ErrorMessage = "Token contains invalid characters.")]
        public string token { get; set; }
    }

    public class VerifyTokenInActiveRes
    {
        public bool active { get; set; }
    }

    public class VerifyTokenActiveRes
    {
        public bool active { get; set; }
        public string client_id { get; set; }
        public string username { get; set; }
        public string scope { get; set; }
        public string org_id { get; set; }
    }
}
