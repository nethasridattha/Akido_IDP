using DTPortal.Core.Domain.Services.Communication;
using DTPortal.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services
{
    public interface ISoftwareService
    {
        Task<IEnumerable<SoftwareNewListDTO>> GetAllSoftwareListNewAsync();

        Task<IEnumerable<SoftwareListDTO>> GetAllSoftwareListAsync();

        Task<ServiceResult> PublishUnpublishSoftwareAsync(int id, string status);

        Task<ServiceResult> UploadSoftwareAsync(UploadSoftwareDTO uploadSoftware);
    }
}
