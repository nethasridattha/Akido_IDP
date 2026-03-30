using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Services.Communication;
using static DTPortal.Common.CommonResponse;

namespace DTPortal.Core.Domain.Services
{
    public interface IUserConsoleService
    {
        //Task<Response> ChangePassword(int userId, string oldPassword, string newPassword);
        Task<UserResponse> UpdateProfile(UserTable user);
        Task<UserAuthDataResponse> ProvisionUser(UserAuthDatum userAuthData);
        Task<UserTable> GetUserAsync(int id);
        Task<bool> IsUserProvisioned(UserAuthDatum userAuthData);
        Task<UserAuthDataResponse> GetUserAuthDataAsync(UserAuthDatum userAuthData);
        Task<UserAuthDataResponse> ProvisionExternalUser(UserAuthDatum userAuthData);
    }
}
