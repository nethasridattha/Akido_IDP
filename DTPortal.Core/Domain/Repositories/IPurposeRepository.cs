using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTPortal.Core.Domain.Models;

namespace DTPortal.Core.Domain.Repositories
{
    public interface IPurposeRepository: IGenericRepository<Purpose>
    {
        public Task<bool> IsPurposeExistsWithNameAsync(string name);

        public Task<IEnumerable<Purpose>> ListAllPurposeAsync();

        public Task<Purpose> GetPurposeById(int id);

        public Task<Purpose> GetPurposeByNameAsync(string name);
    }
}
