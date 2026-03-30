using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services.Communication
{
    public class GetWalletSubscriberProfileRequest
    {
        public string DataPivotUid { get; set; }
        public string Suid { get; set; }
        public string AccessToken { get; set; }
        public string UserId { get; set; }
        public string ProfileType { get; set; }
    }
}
