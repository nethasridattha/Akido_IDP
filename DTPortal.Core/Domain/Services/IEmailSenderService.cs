using DTPortal.Core.Domain.Services.Communication;
using DTPortal.Core.DTOs;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services
{
    public interface IEmailSenderService
    {
        Task<ServiceResult> SendEmailAsync(EmailRequestDTO requestData);
    }
}
