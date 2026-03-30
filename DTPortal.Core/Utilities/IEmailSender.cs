using DTPortal.Core.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTPortal.Core.Utilities
{
    public interface IEmailSender
    {
        Task<int> SendEmail(Message message);
    }
}
