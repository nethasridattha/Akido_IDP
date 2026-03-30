using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTPortal.Core.Domain.Models;

namespace DTPortal.Core.Domain.Services.Communication
{
    public class TimeBasedAccessResponse : BaseResponse<TimeBasedAccess>
    {
        public TimeBasedAccessResponse(TimeBasedAccess category) : base(category) { }

        public TimeBasedAccessResponse(string message) : base(message) { }
    }
}
