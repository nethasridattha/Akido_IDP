using DTPortal.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services.Communication
{
    public class UserClaimResponse : BaseResponse<UserClaim>
    {
        public UserClaimResponse(UserClaim category) : base(category) { }

        public UserClaimResponse(string message) : base(message) { }

        public UserClaimResponse(UserClaim category, string message) : base(category, message){ }
    }
}
