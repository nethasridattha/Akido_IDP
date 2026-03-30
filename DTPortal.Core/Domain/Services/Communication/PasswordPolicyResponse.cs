using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTPortal.Core.Domain.Models;

namespace DTPortal.Core.Domain.Services.Communication
{
    public class PasswordPolicyResponse : BaseResponse<PasswordPolicy>
    {
        public PasswordPolicyResponse(PasswordPolicy category) : base(category) { }

        public PasswordPolicyResponse(string message) : base(message) { }
    }
}
