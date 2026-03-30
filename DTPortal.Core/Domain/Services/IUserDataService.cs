using DTPortal.Core.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services
{
    public interface IUserDataService
    {
        public Task<ServiceResult> GetSocialBenefitCardDetails(string userId);
        public Task<ServiceResult> GetProfile(string url);
        public Task<ServiceResult> GetMdlProfile(string userId);
        public Task<ServiceResult> GetPidProfile(string userId);
    }
}
