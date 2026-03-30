using DTPortal.Core.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Repositories
{
    public interface IUserAuthDatumRepository: IGenericRepository<UserAuthDatum>
    {
        Task<bool> IsUserAuthDataExists(string userId, string PrimauthSchm);
        Task<UserAuthDatum> GetUserAuthDataAsync(string userId, string PrimauthSchm);
        Task<UserAuthDatum> GetUserTempAuthDataAsync(string userId, string PrimauthSchm);
        Task<UserAuthDatum> GetUserInactiveAuthDataAsync(string userId, string PrimauthSchm);
        Task<List<UserAuthDatum>> GetAllUserAuthDataAsync(string userId, string PrimauthSchm);
    }
}
