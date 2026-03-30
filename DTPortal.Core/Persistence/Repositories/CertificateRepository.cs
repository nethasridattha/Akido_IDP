using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Persistence.Repositories
{
    public class CertificateRepository : GenericRepository<Certificate, idp_dtplatformContext>,
            ICertificatesRepository
    {
        public CertificateRepository(idp_dtplatformContext context, ILogger logger) : base(context, logger)
        {

        }
        public async Task<Certificate> GetActiveCertificateAsync()
        {
            return await Context.Certificates.
                SingleOrDefaultAsync(u => u.Status == "ACTIVE");
        }

        public Certificate GetActiveCertificate()
        {
            return  Context.Certificates.
                SingleOrDefault(u => u.Status == "ACTIVE");
        }
    }
}
