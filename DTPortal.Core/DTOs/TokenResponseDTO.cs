using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.DTOs
{
    public class TokenResponseDTO
    {
        public string access_token { get; set; }
        public string Scope { get; set; }
        public string TokenType { get; set; }
        public int ExpiresIn { get; set; }
    }
}
