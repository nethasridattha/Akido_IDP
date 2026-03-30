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
    public interface IUserProfileService
    {
        public Task<UserData> GetUserBasicDataAsync(string userId, string userIdType);

        public Task<ServiceResult> GetUserDetailsNira(string userId);

        public Task<APIResponse> GetUserProfileDataNewAsync(GetUserProfileRequest request);

        Task<ServiceResult> GetAgentDetails(GetAgentDetailsDTO request);
        public Task<ServiceResult> GetUserEmail(string suid);
    }
}
