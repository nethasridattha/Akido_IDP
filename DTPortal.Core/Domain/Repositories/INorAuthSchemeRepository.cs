using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTPortal.Core.Domain.Models;

namespace DTPortal.Core.Domain.Repositories
{
    public interface INorAuthSchemeRepository : IGenericRepository<NorAuthScheme>
    {
        Task<IEnumerable<NorAuthScheme>> GetPrimaryAuthSchemeIds(int authSchemeId);

        Task<IEnumerable<NorAuthScheme>> GetNorAuthSchmIDsbyAuthSchm(int AuthSchmId);

        Task DeleteNorAuthSchmIDsbyAuthSchm(int AuthSchmId);
    }
}
