using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Repositories;
using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Services.Communication;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace DTPortal.Core.Utilities
{
    public class PKILibrary : IPKILibrary
    {
        // Initialize logger.
        private readonly ILogger<PKILibrary> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public static bool pkiLibraryInit = false;
        private readonly IConfigurationService _configurationService;
        private readonly ICertificateService _certificateService;
        private readonly SSOConfig ssoConfig;
        private readonly IGlobalConfiguration _globalConfiguration;
        private readonly ICertificateIssuanceService _certificateIssuanceService;

        public PKILibrary(IUnitOfWork unitOfWork, ILogger<PKILibrary> logger,
            IConfigurationService configurationService,
            ICertificateService certificateService,
            IGlobalConfiguration globalConfiguration,
            ICertificateIssuanceService certificateIssuanceService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _configurationService = configurationService;
            _certificateService = certificateService;
            _globalConfiguration = globalConfiguration;
            _certificateIssuanceService = certificateIssuanceService;
            _logger.LogDebug("-->PKILibrary");

            // Get SSO Configuration
            ssoConfig = _globalConfiguration.GetSSOConfiguration();
            if (null == ssoConfig)
            {
                _logger.LogError("Get SSO Configuration failed in pki library");
                throw new NullReferenceException();
            }

            if (true == ssoConfig.sso_config.remoteSigning &&
                 false == pkiLibraryInit)
            {
                _logger.LogDebug("Initializing PKI Library");

                pkiLibraryInit = true;

                _logger.LogInformation("PKI Library Initialized");
            }
            _logger.LogDebug("<--PKILibrary");
        }

        public int CheckAndCreateCertificate()
        {
            _logger.LogDebug("-->CheckAndCreateCertificate");

            // Local variable declaration
            int result = -1;

            // Local variable declaration
            bool createCertificate = false;

            // Get Active IDP certificate
            var activeCertificate = _unitOfWork.Certificates.
                GetActiveCertificate();
            if (null == activeCertificate)
            {
                // No Active Certificate
                // Create New certificate
                createCertificate = true;
                _logger.LogInformation("No Active Certificate");
            }

            if (null != activeCertificate)
            {
                bool certificateExpired = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified) >=
                    activeCertificate.ExpiryDate;
                if (true == certificateExpired)
                {
                    // Create Certificate request
                    var certificateConfig = new PKIIssueCertificateReq();
                    certificateConfig.commonName = "DAES";
                    certificateConfig.keyID = Guid.NewGuid().ToString();
                    certificateConfig.daesCertificate = true;
                    certificateConfig.countryName = "UG";

                    // Convert Certificate request object to string
                    var certificateRequest = JsonConvert.SerializeObject(
                        certificateConfig);
                    if (null == certificateRequest)
                    {
                        _logger.LogError("Convert Certificate request" +
                            "object to string failed");
                        return result;
                    }

                    // Create New Certificate
                    var response = IssueCertificate(certificateRequest);
                    if (null == response)
                    {
                        _logger.LogError("IssueCertificate failed");
                        return result;
                    }

                    // Convert response string to Object
                    var responseObj = JsonConvert.DeserializeObject
                        <PKIIssueCertificateRes>(response);
                    if (null == responseObj)
                    {
                        _logger.LogError("Convert response string to Object failed");
                        return result;
                    }

                    // Convert string to DateTime Object
                    CultureInfo provider = CultureInfo.InvariantCulture;
                    DateTime issueDate = new DateTime();
                    DateTime expiryDate = new DateTime();

                    try
                    {
                        issueDate = DateTime.ParseExact(responseObj.issueDate,
                        new string[] { "MM/dd/yyyy" }, provider, DateTimeStyles.None);
                        expiryDate = DateTime.ParseExact(responseObj.expiryDate,
                        new string[] { "MM/dd/yyyy" }, provider, DateTimeStyles.None);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("DateTime.ParseExact Failed:{0}",
                            ex.Message);
                        return result;
                    }

                    // Create certificate Object
                    var certificate = new Certificate();
                    certificate.SerialNumber = responseObj.certificate_serial_number;
                    certificate.Data = responseObj.certificate;
                    certificate.Kid = certificateConfig.keyID;
                    certificate.Status = "ACTIVE";
                    certificate.IssueDate = issueDate;
                    certificate.ExpiryDate = expiryDate;
                    certificate.CreatedDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                    certificate.ModifiedDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);

                    // Add certificate in Database
                    _unitOfWork.Certificates.Add(certificate);
                    _unitOfWork.Save();

                    // Get JWKS Configuration
                    var jwks = _configurationService.GetConfiguration
                        <JwksKey>("Jwks_Config");
                    if (null == jwks)
                    {
                        _logger.LogError("GetJWKSConfig failed");
                        return result;
                    }

                    // Add JWK in the JWKS list
                    jwks.keys.Add(responseObj.kidData);

                    // Set JWKS Configuration
                    var responseData = _configurationService.SetConfiguration(
                        "Jwks_Config", jwks);
                    if (null == responseData)
                    {
                        _logger.LogError("SetJWKSConfig failed");
                        return result;
                    }

                    _logger.LogDebug("<--CheckAndCreateCertificate");

                    // Return Success
                    //result = 0;
                    //return result;
                    // Active certificate was expired.
                    _logger.LogInformation("Active Certificate Expired");
                    activeCertificate.Status = "EXPIRED";

                    // Update certificate status in Database
                    _unitOfWork.Certificates.Update(activeCertificate);
                    _unitOfWork.Save();

                    _logger.LogInformation("Old Certificate Status Updated to Expired");

                    createCertificate = true;
                    result = 0;
                }
            }

            if (false == createCertificate)
            {
                _logger.LogInformation("Active Certificate" +
                    "Exists and not expired");

                // Return Success
                result = 0;
                return result;
            }
            return result;
        }

        public async Task<ServiceResult> CheckAndCreateCertificateNew()
        {

            var activeCertificate =await _unitOfWork.Certificates.
                GetActiveCertificateAsync();
            if (null == activeCertificate)
            {
                _logger.LogInformation("No Active Certificate");
            }

            if (null != activeCertificate)
            {
                bool certificateExpired = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified) >=
                    activeCertificate.ExpiryDate;
                if (true == certificateExpired)
                {
                    var certificateConfig = new CertificateIssueRequest();
                    certificateConfig.identifier = Guid.NewGuid().ToString();
                    certificateConfig.certSubject = "DAES";
                    certificateConfig.certProcedure = "SHA256RSA";
                    certificateConfig.country = "IN";
                    certificateConfig.tokenCert = true;

                    var response = await _certificateIssuanceService.
                        IssueCertificateNew(certificateConfig);
                    if (null == response || !response.Success)
                    {
                        _logger.LogError("IssueCertificate failed");
                        throw new Exception();
                    }

                    var responseObj = (CertificateResult)response.Resource;

                    if (null == responseObj)
                    {
                        _logger.LogError("Convert response string to Object failed");
                        throw new Exception();
                    }

                    CultureInfo provider = CultureInfo.InvariantCulture;
                    DateTime issueDate = new DateTime();
                    DateTime expiryDate = new DateTime();

                    try
                    {
                        issueDate = DateTime.ParseExact(responseObj.CertData.IssueDate.ToString(),
                        new string[] { "MM/dd/yyyy" }, provider, DateTimeStyles.None);
                        expiryDate = DateTime.ParseExact(responseObj.CertData.ExpiryDate.ToString(),
                        new string[] { "MM/dd/yyyy" }, provider, DateTimeStyles.None);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("DateTime.ParseExact Failed:{0}",
                            ex.Message);
                        throw new Exception();
                    }

                    var certificate = new Certificate();
                    certificate.SerialNumber = responseObj.CertData.SerialNumber;
                    certificate.Data = responseObj.CertData.Certificate;
                    certificate.Kid = responseObj.KidData.kid;
                    certificate.Status = "ACTIVE";
                    certificate.IssueDate = issueDate;
                    certificate.ExpiryDate = expiryDate;
                    certificate.CreatedDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                    certificate.ModifiedDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                    try
                    {
                        await _unitOfWork.Certificates.AddAsync(certificate);
                        await _unitOfWork.SaveAsync();
                    }
                    catch(Exception ex)
                    {
                        _logger.LogError("Add Certificate to DB Failed:{0}",
                            ex.Message);
                        throw new Exception();
                    }

                    var jwks =await _configurationService.GetConfigurationAsync
                        <JwksKey>("Jwks_Config");
                    if (null == jwks)
                    {
                        _logger.LogError("GetJWKSConfig failed");
                        throw new Exception();
                    }

                    jwks.keys.Add(responseObj.KidData);

                    var responseData = _configurationService.SetConfiguration(
                        "Jwks_Config", jwks);
                    if (null == responseData)
                    {
                        _logger.LogError("SetJWKSConfig failed");
                        throw new Exception();
                    }

                    _logger.LogDebug("<--CheckAndCreateCertificate");

                    _logger.LogInformation("Active Certificate Expired");
                    activeCertificate.Status = "EXPIRED";
                    try
                    {
                        _unitOfWork.Certificates.Update(activeCertificate);
                        await _unitOfWork.SaveAsync();
                    }
                    catch(Exception ex)
                    {
                        _logger.LogError("Update Old Certificate Status Failed:{0}",
                            ex.Message);
                        throw new Exception();
                    }

                    _logger.LogInformation("Old Certificate Status Updated to Expired");

                    return new ServiceResult(true, "Certificate Created Successfully");
                }
                return new ServiceResult(true, "Certificate is Active");
            }

            else
            {
                var certificateConfig = new CertificateIssueRequest();
                certificateConfig.identifier = "208";
                certificateConfig.certSubject = "DAES";
                certificateConfig.certProcedure = "SHA256RSA";
                certificateConfig.country = "IN";
                certificateConfig.tokenCert = true;

                var response = await _certificateIssuanceService.
                    IssueCertificateNew(certificateConfig);
                if (null == response || !response.Success)
                {
                    _logger.LogError("IssueCertificate failed");
                    throw new Exception();
                }

                var responseObj = (CertificateResult)response.Resource;

                if (null == responseObj)
                {
                    _logger.LogError("Convert response string to Object failed");
                    throw new Exception();
                }

                CultureInfo provider = CultureInfo.InvariantCulture;
                DateTime issueDate = new DateTime();
                DateTime expiryDate = new DateTime();

                try
                {
                    issueDate = DateTime.ParseExact(responseObj.CertData.IssueDate.ToString(),
                    new string[] { "MM/dd/yyyy" }, provider, DateTimeStyles.None);
                    expiryDate = DateTime.ParseExact(responseObj.CertData.ExpiryDate.ToString(),
                    new string[] { "MM/dd/yyyy" }, provider, DateTimeStyles.None);
                }
                catch (Exception ex)
                {
                    _logger.LogError("DateTime.ParseExact Failed:{0}",
                        ex.Message);
                    throw new Exception();
                }

                var certificate = new Certificate();
                certificate.SerialNumber = responseObj.CertData.SerialNumber;
                certificate.Data = responseObj.CertData.Certificate;
                certificate.Kid = responseObj.KidData.kid;
                certificate.Status = "ACTIVE";
                certificate.IssueDate = issueDate;
                certificate.ExpiryDate = expiryDate;
                certificate.CreatedDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                certificate.ModifiedDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                try
                {
                    await _unitOfWork.Certificates.AddAsync(certificate);
                    await _unitOfWork.SaveAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Add Certificate to DB Failed:{0}",
                        ex.Message);
                    throw new Exception();
                }
                return new ServiceResult(true, "Certificate Created Successfully");
            }
        }

        // Create new certificate.
        public async Task<int> CreateCertificateAsync(
            PKIIssueCertificateReq pkiIssueCertificate)
        {
            _logger.LogDebug("-->CreateCertificate");
            // Local variable declaration
            int result = -1;

            // Validate input parameters
            if (null == pkiIssueCertificate)
            {
                _logger.LogError("Invalid Input Parameter");
                return result;
            }

            // Convert Certificate request object to string
            var certificateRequest = JsonConvert.SerializeObject(
                pkiIssueCertificate);
            if (null == certificateRequest)
            {
                _logger.LogError("Convert Certificate request" +
                    "object to string failed");
                return result;
            }

            // Create New Certificate
            var response = IssueCertificate(certificateRequest);
            if (null == response)
            {
                _logger.LogError("IssueCertificate failed");
                return result;
            }

            // Convert response string to Object
            var responseObj = JsonConvert.DeserializeObject
                <PKIIssueCertificateRes>(response);
            if (null == responseObj)
            {
                _logger.LogError("Convert response string to Object failed");
                return result;
            }

            // Convert string to DateTime Object
            CultureInfo provider = CultureInfo.InvariantCulture;
            DateTime issueDate = new DateTime();
            DateTime expiryDate = new DateTime();

            try
            {
                issueDate = DateTime.ParseExact(responseObj.issueDate,
                new string[] { "MM/dd/yyyy" }, provider, DateTimeStyles.None);
                expiryDate = DateTime.ParseExact(responseObj.expiryDate,
                new string[] { "MM/dd/yyyy" }, provider, DateTimeStyles.None);
            }
            catch (Exception ex)
            {
                _logger.LogError("DateTime.ParseExact Failed:{0}",
                    ex.Message);
                return result;
            }

            // Create certificate Object
            var certificate = new Certificate();
            certificate.SerialNumber = responseObj.certificate_serial_number;
            certificate.Data = responseObj.certificate;
            certificate.Kid = pkiIssueCertificate.keyID;
            certificate.Status = "ACTIVE";
            certificate.IssueDate = issueDate;
            certificate.ExpiryDate = expiryDate;
            certificate.CreatedDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            certificate.ModifiedDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);

            // Add certificate in Database
            await _unitOfWork.Certificates.AddAsync(certificate);
            await _unitOfWork.SaveAsync();

            // Get JWKS Configuration
            var jwks = await _configurationService.GetConfigurationAsync
                <JwksKey>("Jwks_Config");
            if (null == jwks)
            {
                _logger.LogError("GetJWKSConfig failed");
                return result;
            }

            // Add JWK in the JWKS list
            jwks.keys.Add(responseObj.kidData);

            // Set JWKS Configuration
            var responseData = await _configurationService.SetConfigurationAsync
                ("Jwks_Config", jwks, "sysadmin");
            if (null == responseData)
            {
                _logger.LogError("SetJWKSConfig failed");
                return result;
            }

            // Return Success
            result = 0;
            _logger.LogDebug("<--CreateCertificate");
            return result;
        }

        public async Task<ServiceResult> GenerateCertificateAsync()
        {
            var certificateConfig = new CertificateIssueRequest();
            certificateConfig.identifier = "208";
            certificateConfig.certSubject = "DAES";
            certificateConfig.certProcedure = "SHA256RSA";
            certificateConfig.country = "IN";
            certificateConfig.tokenCert = true;

            var response = await _certificateIssuanceService.
                IssueCertificateNew(certificateConfig);
            if (null == response || !response.Success)
            {
                _logger.LogError("IssueCertificate failed");
                return new ServiceResult(false, "IssueCertificate failed");
            }

            var responseObj = (CertificateResult)response.Resource;

            if (null == responseObj)
            {
                _logger.LogError("Convert response string to Object failed");
                return new ServiceResult(false, "Convert response string to Object failed");
            }

            CultureInfo provider = CultureInfo.InvariantCulture;

            var certificate = new Certificate();
            certificate.SerialNumber = responseObj.CertData.SerialNumber;
            certificate.Data = responseObj.CertData.Certificate;
            certificate.Kid = responseObj.KidData.kid;
            certificate.Status = "ACTIVE";
            certificate.IssueDate = responseObj.CertData.IssueDate;
            certificate.ExpiryDate = responseObj.CertData.ExpiryDate;
            certificate.CreatedDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            certificate.ModifiedDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            try
            {
                await _unitOfWork.Certificates.AddAsync(certificate);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Add Certificate to DB Failed:{0}",
                    ex.Message);
                return new ServiceResult(false, ex.Message);
            }

            var jwks =await  _configurationService.GetConfigurationAsync
                <JwksKey>("Jwks_Config");
            if (null == jwks)
            {
                _logger.LogError("GetJWKSConfig failed");
                return new ServiceResult(false, "GetJWKSConfig failed");
            }

            jwks.keys.Add(responseObj.KidData);

            var responseData = _configurationService.SetConfiguration(
                "Jwks_Config", jwks);
            if (null == responseData)
            {
                _logger.LogError("SetJWKSConfig failed");
                return new ServiceResult(false, "SetJWKSConfig failed");
            }

            _logger.LogInformation("Old Certificate Status Updated to Expired");

            return new ServiceResult(true, "Certificate Created Successfully");
        }

        // Initialize PKI Library
        private int InitializePKI(string data)
        {
            _logger.LogDebug("-->InitializePKI");

            // Local variable declaration
            int result = -1;

            // Validate input parameters
            if (string.IsNullOrEmpty(data))
            {
                _logger.LogError("Invalid Input Parameter");
                return result;
            }

            try
            {
                // Initialize Native Library
                int retValue = PKINativeMethods.InitializePKIServiceNative(data,
                    IntPtr.Zero);
                if (retValue != 0)
                {
                    _logger.LogError("Failed to initialize PKI Service.");
                    return retValue;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("InitializePKI Exception:{0}", ex.Message);
                Monitor.SendException(ex);
                return result;
            }

            // Return Success
            result = 0;

            _logger.LogDebug("<--InitializePKI");
            return result;
        }

        // Generates Signature for given data
        public string GenerateSignature(string data)
        {
            _logger.LogDebug("-->GenerateSignature");

            // local variables
            string response = null;
            IntPtr responseBuffer = IntPtr.Zero;
            int responseBufferLength = 0;

            // Validate input parameters
            if (string.IsNullOrEmpty(data))
            {
                _logger.LogError("Invalid Input Parameter");
                return response;
            }

            try
            {
                int result = PKINativeMethods.GenerateSignatureNative(
                     data,
                     ref responseBuffer,
                     ref responseBufferLength);
                if (result != 0)
                {
                    string error = GetStatusMessagePKI(result);
                    if (null != error)
                        _logger.LogError("GenerateSignature Failed :{0}",
                            error);
                    return response;
                }

                response = Marshal.PtrToStringAnsi(responseBuffer,
                    responseBufferLength);

            }
            catch (Exception ex)
            {
                _logger.LogError("GenerateSignature Exception: {0}",
                    ex.Message);
            }
            finally
            {
                if (responseBuffer != IntPtr.Zero)
                {
                    // Free the buffer
                    FreeMemoryPKI(responseBuffer);
                    responseBuffer = IntPtr.Zero;
                }
            }

            _logger.LogDebug("<--GenerateSignature");
            return response;
        }

        // Creates new certificate key pair and returns
        // public key details
        public string IssueCertificate(string certificateRequest)
        {
            _logger.LogDebug("-->IssueCertificate");

            // local variables
            string response = null;
            IntPtr responseBuffer = IntPtr.Zero;
            int responseBufferLength = 0;

            // Validate input parameters
            if (string.IsNullOrEmpty(certificateRequest))
            {
                _logger.LogError("Invalid Input Parameter");
                return response;
            }

            try
            {
                int result = PKINativeMethods.IssueCertificateNative(
                     certificateRequest,
                     ref responseBuffer,
                     ref responseBufferLength);
                if (result != 0)
                {
                    string error = GetStatusMessagePKI(result);
                    if (null != error)
                        _logger.LogError("IssueCertificate Failed :{0}",
                            error);
                    Monitor.SendMessage(error);
                    return response;
                }

                response = Marshal.PtrToStringAnsi(responseBuffer,
                    responseBufferLength);
            }
            catch (Exception ex)
            {
                _logger.LogError("IssueCertificate Exception: {0}",
                    ex.Message);
                Monitor.SendException(ex);
            }
            finally
            {
                if (responseBuffer != IntPtr.Zero)
                {
                    // Free the buffer
                    FreeMemoryPKI(responseBuffer);
                    responseBuffer = IntPtr.Zero;
                }
            }
            _logger.LogDebug("<--IssueCertificate");
            return response;
        }

        // Get Error Message from Error Code
        private string GetStatusMessagePKI(int errorCode)
        {
            _logger.LogDebug("-->GetStatusMessagePKI");
            string response = null;
            IntPtr responseBuffer = IntPtr.Zero;

            try
            {
                responseBuffer = PKINativeMethods.GetStatusMsgPKIServiceNative(errorCode);
                response = Marshal.PtrToStringAnsi(responseBuffer);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetStatusMessagePKI Exception:{0}",
                    ex.Message);
                return response;
            }

            _logger.LogDebug("<--GetStatusMessagePKI");
            return response;
        }

        // Free memory
        private void FreeMemoryPKI(IntPtr buffer)
        {
            _logger.LogDebug("-->FreeMemoryPKI");
            try
            {
                int result = PKINativeMethods.FreeMemoryPKIServiceNative(buffer);
                if (result != 0)
                {
                    string error = GetStatusMessagePKI(result);
                    if (null != error)
                        _logger.LogError("FreeMemoryPKIServic Failed:{0}",
                            error);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("FreeMemoryPKIServic Exception:{0}",
                    ex.Message);
                return;
            }
            _logger.LogDebug("<--FreeMemoryPKI");
        }

        // CleanUp PKI Library
        private void CleanupPKI()
        {
            _logger.LogDebug("-->CleanupPKI");
            try
            {
                int result = PKINativeMethods.CleanupPKIServiceNative();
                if (result != 0)
                {
                    string error = GetStatusMessagePKI(result);
                    if (null != error)
                        _logger.LogError("CleanupPKI Failed:{0}", error);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("CleanupPKI Exception:{0}", ex.Message);
                return;
            }

            _logger.LogDebug("<--CleanupPKI");
        }
    }
}
