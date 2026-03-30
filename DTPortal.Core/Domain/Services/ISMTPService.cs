using System.Threading.Tasks;

using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Services.Communication;


namespace DTPortal.Core.Domain.Services
{
    public interface ISMTPService
    {
        Task<Smtp> GetSMTPSettingsAsync(int id);

        Task<SMTPResponse> UpdateSMTPSettingsAsync(Smtp smtp);

        Task<SMTPResponse> TestSMTPConnectionAsync(Smtp smtp);
    }
}
