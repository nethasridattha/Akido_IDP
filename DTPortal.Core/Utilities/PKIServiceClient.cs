using DTPortal.Core.Constants;
using DTPortal.Core.Domain.Repositories;
using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Services.Communication;
using DTPortal.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Utilities
{
    public class PKIServiceClient : IPKIServiceClient
    {
        private readonly SSOConfig ssoConfig;
        private readonly ILogger<PKIServiceClient> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGlobalConfiguration _globalConfiguration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly MessageConstants Constants;
        private readonly IHelper _helper;

        public PKIServiceClient(ILogger<PKIServiceClient> logger,
            IUnitOfWork unitOfWork,
            IGlobalConfiguration globalConfiguration,
            IHttpClientFactory httpClientFactory,
            IHelper helper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _globalConfiguration = globalConfiguration;
            _httpClientFactory = httpClientFactory;
            _helper = helper;

            _logger.LogDebug("-->PKIServiceClient");

            // Get SSO Configuration
            ssoConfig = _globalConfiguration.GetSSOConfiguration();
            if (null == ssoConfig)
            {
                _logger.LogError("Get SSO Configuration failed in pkiservice client");
                throw new NullReferenceException();
            }

            var errorConfiguration = _globalConfiguration.GetErrorConfiguration();
            if (null == errorConfiguration)
            {
                _logger.LogError("Get Error Configuration failed");
                throw new NullReferenceException();
            }

            Constants = errorConfiguration.Constants;
            if (null == Constants)
            {
                _logger.LogError("Get Error Configuration failed");
                throw new NullReferenceException();
            }

            _logger.LogDebug("<--PKIServiceClient");
        }

        // Generate base 64 encoded string of random byte array
        private string GenerateRandomNumber(int bytes)
        {
            _logger.LogDebug("-->GenerateRandomNumber");

            if (bytes <= 0)
                throw new ArgumentOutOfRangeException(nameof(bytes));

            try
            {
                byte[] buffer = new byte[bytes];
                RandomNumberGenerator.Fill(buffer);

                return Convert.ToBase64String(buffer);
            }
            catch (Exception ex)
            {
                _logger.LogError("GenerateRandomNumber Failed:{Error}", ex.Message);
                return null;
            }
            finally
            {
                _logger.LogDebug("<--GenerateRandomNumber");
            }
        }

        // Get signature for a random number by verifying user authentication pin
        public async Task<VerifyPinResponse> GenerateSignature(
            string address, string requestUri,
            GenerateSignatureRequest generateSignatureRequest)
        {
            _logger.LogDebug("-->GenerateSignature");

            // Local Variable Declaration
            VerifyPinResponse response = null;
            var errorMessage = string.Empty;

            if (string.IsNullOrEmpty(address) ||
                string.IsNullOrEmpty(requestUri) ||
                null == generateSignatureRequest)
            {
                _logger.LogError("Invalid Input Parameter");
                return response;
            }

            _logger.LogDebug("Base Address: {0}", address);
            _logger.LogDebug("Generate Signature Uri: {0}",
                requestUri);
            try
            {
                HttpClient client = _httpClientFactory.CreateClient();

                // Assign the base address
                client.BaseAddress = new Uri(address);

                // Set Request Timeout
                client.Timeout = TimeSpan.FromSeconds(51);

                // Call the webservice with post method
                var result = await client.PostAsJsonAsync<GenerateSignatureRequest>
                    (requestUri, generateSignatureRequest);

                // Check the status code
                if (result.IsSuccessStatusCode)
                {
                    // Read the response
                    response = await result.Content.ReadFromJsonAsync
                        <VerifyPinResponse>();
                }
                else
                {
                    _logger.LogError("GenerateSignature failed returned" +
                        ",Status code : {0}", result.StatusCode);
                    Monitor.SendMessage("VerifySignature failed returned status code : {0}" +
                        result.StatusCode);
                    var errorCode = ErrorCodes.PKI_SERVICE_GEN_SIGNATURE_FAILED;
                    if (HttpStatusCode.ServiceUnavailable == result.StatusCode)
                        errorCode = ErrorCodes.PKI_SERVICE_GEN_SIGNATURE_UNAVAILABLE;
                    response = new VerifyPinResponse();
                    errorMessage = _helper.GetErrorMsg(errorCode);
                    response.message = errorMessage;
                    return response;
                }
            }
            catch (TimeoutException error)
            {
                _logger.LogError("VerifySignature failed due to timeout exception: {0}",
                    error.Message);
                response = new VerifyPinResponse();
                response.success = false;
                errorMessage = _helper.GetErrorMsg(
                    ErrorCodes.PKI_SERVICE_GEN_SIGNATURE_TIMEOUT);
                response.message = errorMessage;
                return response;
            }
            catch (Exception error)
            {
                _logger.LogError("GenerateSignature failed: {0}", error.Message);
                response = new VerifyPinResponse();
                errorMessage = _helper.GetErrorMsg(
                    ErrorCodes.PKI_SERVICE_GEN_SIGNATURE_FAILED);
                response.message = errorMessage;
                return response;
            }

            _logger.LogDebug("<--GenerateSignature");
            return response;
        }

        // Verify the signature
        private async Task<VerifyPinResponse> VerifySignature(
            string address, string requestUri,
            VerifySignatureRequest verifySignatureRequest)
        {
            _logger.LogDebug("-->VerifySignature");

            // Local Variable Declaration
            VerifyPinResponse response = null;
            var errorMessage = string.Empty;

            if (string.IsNullOrEmpty(address) || string.IsNullOrEmpty(requestUri) ||
                null == verifySignatureRequest)
            {
                _logger.LogError("VerifySignature failed:Invalid Parameter");
                return null;
            }

            _logger.LogDebug("Base Address: {0}", address);
            _logger.LogDebug("Generate Signature Uri: {0}",
                requestUri);

            try
            {
                HttpClient client = _httpClientFactory.CreateClient();

                // Assign the base address
                client.BaseAddress = new Uri(address);

                // Set Request Timeout
                client.Timeout = TimeSpan.FromSeconds(51);

                // Call the webservice with post method
                var result = await client.PostAsJsonAsync<VerifySignatureRequest>
                    (requestUri, verifySignatureRequest);

                // Check the status code
                if (result.IsSuccessStatusCode)
                {
                    // Read the response
                    response = await result.Content.ReadFromJsonAsync
                        <VerifyPinResponse>();
                }
                else
                {
                    _logger.LogError(
                        "VerifySignature failed returned status code : {0}",
                        result.StatusCode);
                    Monitor.SendMessage("VerifySignature failed returned status code : {0}" +
                        result.StatusCode);
                    var errorCode = ErrorCodes.PKI_SERVICE_VERIFY_SIGNATURE_FAILED;
                    if (HttpStatusCode.ServiceUnavailable == result.StatusCode)
                        errorCode = ErrorCodes.PKI_SERVICE_VERIFY_SIGNATURE_UNAVAILABLE;
                    response = new VerifyPinResponse();
                    errorMessage = _helper.GetErrorMsg(errorCode);
                    response.message = errorMessage;
                    return response;
                }
            }
            catch (TimeoutException error)
            {
                _logger.LogError("VerifySignature failed due to timeout exception: {0}",
                    error.Message);
                response = new VerifyPinResponse();
                response.success = false;
                errorMessage = _helper.GetErrorMsg(
                    ErrorCodes.PKI_SERVICE_VERIFY_SIGNATURE_TIMEOUT);
                response.message = errorMessage;
                return response;
            }
            catch (Exception error)
            {
                _logger.LogError("VerifySignature failed: {0}", error.Message);
                response = new VerifyPinResponse();
                errorMessage = _helper.GetErrorMsg(
                    ErrorCodes.PKI_SERVICE_VERIFY_SIGNATURE_FAILED);
                response.message = errorMessage;
                return response;
            }

            _logger.LogDebug("<--VerifySignature");
            return response;
        }

        // PKI Authentication
        public async Task<VerifyPinResponse> VerifyPin(
            VerifyPinRequest verifyPinRequest)
        {
            _logger.LogDebug("-->VerifyPin");

            // Local Variable Declaration
            VerifyPinResponse response = null;

            if (null == verifyPinRequest)
            {
                _logger.LogError("VerifyPin failed:Invalid Parameter");
                return response;
            }

            // Generate signature
            GenerateSignatureRequest generateSignatureRequest =
                new GenerateSignatureRequest();
            generateSignatureRequest.subscriberUniqueId =
                verifyPinRequest.subscriberDigitalID;
            generateSignatureRequest.signingPassword =
                verifyPinRequest.authenticationPin;
            generateSignatureRequest.hash = GenerateRandomNumber(8);
            generateSignatureRequest.hashData = false;
            generateSignatureRequest.certType = 1;
            generateSignatureRequest.startTime = DateTime.Now.ToString();
            generateSignatureRequest.transactionID = Guid.NewGuid().ToString();
            generateSignatureRequest.correlationId = verifyPinRequest.correlationId;
            generateSignatureRequest.logMessage = "GENERATE SIGNATURE";
            generateSignatureRequest.logMessageType = "REQUEST";
            generateSignatureRequest.transactionType = "BUSINESS";
            generateSignatureRequest.serviceName = "IDP";

            // Generate Signature
            response = await GenerateSignature(ssoConfig.pki_service_config.base_address,
                ssoConfig.pki_service_config.generate_signature_uri,
                generateSignatureRequest);
            if (null == response)
            {
                _logger.LogError("GenerateSignature failed");
                return response;
            }

            if (true != response.success)
            {
                _logger.LogWarning("GenerateSignature failed");
                return response;
            }

            // Verify signature
            VerifySignatureRequest verifySignatureRequest =
                new VerifySignatureRequest();
            verifySignatureRequest.subscriberUniqueId =
                generateSignatureRequest.subscriberUniqueId;
            verifySignatureRequest.random = generateSignatureRequest.hash;
            verifySignatureRequest.signature = response.result;
            verifySignatureRequest.hashData = false;
            verifySignatureRequest.certType = 1;
            verifySignatureRequest.serviceName = "IDP";
            verifySignatureRequest.startTime = DateTime.Now.ToString();
            verifySignatureRequest.transactionType = "BUSINESS";
            verifySignatureRequest.transactionID = Guid.NewGuid().ToString();
            verifySignatureRequest.correlationId = verifyPinRequest.correlationId;
            verifySignatureRequest.logMessage = "VERIFY SIGNATURE";
            verifySignatureRequest.logMessageType = "REQUEST";

            // Verify Signature
            response = await VerifySignature(ssoConfig.pki_service_config.base_address,
                ssoConfig.pki_service_config.verify_signature_uri,
                verifySignatureRequest);
            if (null == response)
            {
                _logger.LogError("VerifySignature failed");
                return response;
            }

            _logger.LogDebug("<--VerifyPin");
            return response;
        }
    }
}