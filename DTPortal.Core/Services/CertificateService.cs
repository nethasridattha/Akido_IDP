using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Repositories;
using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Services.Communication;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DTPortal.Common.CommonResponse;

namespace DTPortal.Core.Services
{
    public class CertificateService: ICertificateService
    {
        // Initialize logger
        private readonly ILogger<CertificateService> _logger;
        // Initialize Db
        private readonly IUnitOfWork _unitOfWork;

        public CertificateService(ILogger<CertificateService> logger,
            IUnitOfWork unitofWork)
        {
            _logger = logger;
            _unitOfWork = unitofWork;
        }

        public async Task<CertificateResponse> DeactivateCertificateAsync(int id)
        {
            var certificate = await _unitOfWork.Certificates.GetByIdAsync(id);
            if (certificate == null)
            {
                _logger.LogError("Certificate not found");
                return null;
            }

            certificate.Status = "EXPIRED";

            try
            {
                _unitOfWork.Certificates.Update(certificate);
                await _unitOfWork.SaveAsync();

                return new CertificateResponse(certificate);
            }
            catch (Exception ex)
            {
                _logger.LogError("Deactivate Certificate failed:{0}",
                    ex.Message);
                // Do some logging stuff
                return null;
            }
        }

        public  CertificateResponse DeactivateCertificate(int id)
        {
            var certificate = _unitOfWork.Certificates.GetById(id);
            if (certificate == null)
            {
                _logger.LogError("Certificate not found");
                return null;
            }

            certificate.Status = "DEACTIVE";

            try
            {
                _unitOfWork.Certificates.Update(certificate);
                _unitOfWork.Save();

                return new CertificateResponse(certificate);
            }
            catch (Exception ex)
            {
                _logger.LogError("Deactivate Certificate failed:{0}",
                    ex.Message);
                // Do some logging stuff
                return null;
            }
        }

        public async Task<Certificate> GetIdpActiveCertificateAsync()
        {
            try
            {
                return await _unitOfWork.Certificates.GetActiveCertificateAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("GetActiveCertificateAsync failed:{0}",
                    ex.Message);
                return null;
            }
        }

    }
}
