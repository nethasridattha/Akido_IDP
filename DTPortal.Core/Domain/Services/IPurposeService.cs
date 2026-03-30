using System;
using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Services.Communication;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Services
{
    public interface IPurposeService
    {
        public Task<PurposeResponse> CreatePurposeAsync(Purpose purpose);

        public Task<Purpose> GetPurposeAsync(int id);

        public Task<int> GetPurposeIdByNameAsync(string name);

        public Task<PurposeResponse> UpdatePurposeAsync(Purpose purpose);

        public Task<IEnumerable<Purpose>> GetPurposeListAsync();
        public Task<IEnumerable<string>> GetPurposesListAsync();

        public Task<PurposeResponse> DeletePurposeAsync(int id,string UUID);
    }
}
