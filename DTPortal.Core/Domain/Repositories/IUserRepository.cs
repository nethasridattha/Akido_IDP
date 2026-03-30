using System.Threading.Tasks;
using System.Collections.Generic;

using DTPortal.Core.Domain.Models;


namespace DTPortal.Core.Domain.Repositories
{
    public interface IUserRepository : IGenericRepository<UserTable>
    {
        Task<PaginatedList<UserTable>> GetAllUsersWithRolesAsync(int offset, int count);

        Task<UserTable> GetUserByIdWithRoleAsync(int id);

        Task<bool> IsUserExists(UserTable user);
        Task<bool> IsUserExistsWithEmail(UserTable user);
        Task<bool> IsUserExistsWitMobile(UserTable user);
        Task<bool> IsUserExistsbyUuidAsync(string Uuid);
        Task<UserTable> GetUserbyNameAsync(string userName);
        Task<UserTable> GetUserbyUuidAsync(string Uuid);
        Task<UserTable> GetUserbyPhonenoAsync(string phoneNo);
        Task<UserTable> GetUserbyEmailAsync(string emailId);
        Task<bool> IsUserExistsbyUserNameAsync(string userName);
        Task<List<UserTable>> GetAllUsersWithRolesAsync();
        Task<List<string>> SearchUserbyEmailAsync(string emailId);

        Task<List<string>> SearchUserbyPhoneAsync(string phone);
    }
}
