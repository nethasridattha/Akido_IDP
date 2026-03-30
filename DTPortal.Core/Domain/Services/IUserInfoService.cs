using DTPortal.Core.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services
{
    public interface IUserInfoService
    {
        Task<Object> GetUserInfo(string AccessToken,bool signed);
        Task<GetUserInfoResponse> GetUserProfile(string GlobalSession);
        Task<APIResponse> GetUserImage(string AccessToken);
        Task<Object> UserProfile(string AccessToken);
        Task<APIResponse> GetUserDetailsAsync(string id);
        Task<APIResponse> GetAdminProfile(string Id);
    }
}
