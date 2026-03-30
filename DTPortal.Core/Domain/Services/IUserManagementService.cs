using System.Threading.Tasks;
using System.Collections.Generic;

using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Lookups;
using DTPortal.Core.Domain.Services.Communication;
using System;
using DTPortal.Core.Domain.Models.RegistrationAuthority;

namespace DTPortal.Core.Domain.Services
{
    public interface IUserManagementService
    {
        Task<PaginatedList<UserTable>> ListUsersAsync(int offset, int count);
        Task<List<UserTable>> ListUsersAsync();
        Task<UserResponse> AddUserAsync(UserTable user, string password,
            bool makerCheckerFlag = false);

        Task<UserResponse> UpdateUserAsync(UserTable user, bool makerCheckerFlag = false);

        Task<UserTable> GetUserAsync(int id);
        Task<UserTable> GetUserAsyncByEmail(string email);
        Task<UserTable> GetUserAsyncByPhone(string phone);
        Task<string> GetUserStatusAsync(int id);
        Task<bool> GetUserFido2StatusAsync(string id);
        Task<SubscriberView> GetSubscriberAsyncBySuid(string suid);
        Task<IEnumerable<RoleLookupItem>> GetRoleLookupsAsync();

        Task<UserResponse> DeleteUserAsync(int id, string updatedBy,
             bool makerCheckerFlag = false);
        Task<UserResponse> VerifyToken(string jwtToken, string issuer, string audience,
                    bool validateIssuer = true, bool validateAudience = true, bool validateExpiry = true);
        Task<UserResponse> SaveUserAsync(UserTable user, string authData);
        Task<UserResponse> SendTempDeviceLinkAsync(
            string user_id,
            string authData,
            DateTime? expiry
            );

        Task<UserResponse> RegisterTempDeviceAsync(
            string user_id,
            string authData
            );

        Task<UserResponse> DeactivateUserAsync(int id);
        Task<UserResponse> ActivateUserAsync(int id);

        Task<UserResponse> VerifyDeviceRegistrationToken(string jwtToken);
        Task<UserTable> GetUserAsyncByName(string name);
        Task<UserTable> GetUserAsyncByUid(string uid);
        Task<UserResponse> IntiateFido2DeviceAsync(
            string user_id,
            string authData,
            DateTime? expiry
            );
        Task<UserResponse> RegisterUserFido2DeviceAsync(
            string user_id,
            string authData,
            DateTime? expiry
            );

        Task<UserResponse> AdminResetPassword(int userId);
        Task<List<string>> SearchUserAsyncByEmail(string email);
        Task<List<string>> SearchUserAsyncByPhone(string phone);
    }
}
