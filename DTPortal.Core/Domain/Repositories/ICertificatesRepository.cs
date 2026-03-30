using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTPortal.Core.Domain.Models;

namespace DTPortal.Core.Domain.Repositories
{
    public interface ICertificatesRepository : IGenericRepository<Certificate>
    {
        public Task<Certificate> GetActiveCertificateAsync();

        public Certificate GetActiveCertificate();
    }
}
