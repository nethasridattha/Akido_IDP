using System.Threading.Tasks;

using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Services.Communication;

namespace DTPortal.Core.Domain.Services
{
    public interface IUserAuthDataService
    {
        Task<UserAuthDataResponse> ProvisionUser(UserAuthDatum userAuthData);

        Task SaveAsync();
    }
}
