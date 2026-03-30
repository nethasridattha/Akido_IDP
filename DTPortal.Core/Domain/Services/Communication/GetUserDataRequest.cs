using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services.Communication
{
    public class GetUserDataRequest
    {
        public string ProfileType { get; set; }
        public string Token { get; set; }
        public string Suid { get; set; }
        public string UserId { get; set; }
    }
}
