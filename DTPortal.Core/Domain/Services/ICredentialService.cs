using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Services.Communication;
using DTPortal.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services
{
    public interface ICredentialService
    {
        Task<ServiceResult> GetCredentialOfferByUidAsync(string Id, string token);

    }
}
