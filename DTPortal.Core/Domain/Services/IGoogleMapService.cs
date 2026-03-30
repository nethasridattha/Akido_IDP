using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services
{
    public interface IGoogleMapService
    {
        Task<string> GetAddressByLatitudeLongitude(string latitude, string longitude);
    }
}
