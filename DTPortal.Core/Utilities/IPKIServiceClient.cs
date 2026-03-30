using DTPortal.Core.Services;
using System.Threading.Tasks;

namespace DTPortal.Core.Utilities
{
    public interface IPKIServiceClient
    {
       public Task<VerifyPinResponse> GenerateSignature(
            string address, string requestUri,
        GenerateSignatureRequest generateSignatureRequest);
        public Task<VerifyPinResponse> VerifyPin(VerifyPinRequest verifyPinRequest);
    }
}