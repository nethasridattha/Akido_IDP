using OtpNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Utilities
{
    public class TOTPLibrary
    {
        public static bool VerifyTOTP(string secret_base32String, string code)
        {

            // Convert to base32 encoding bytes
            var secret_base32Bytes = Base32Encoding.ToBytes(secret_base32String);

            // Create TOTP class and store the secret with default utcnow time
            var totp = new Totp(secret_base32Bytes);

            long timeStepMatched = 0;
            VerificationWindow window = null;

            // Verify the TOTP entered by the user
            bool result = totp.VerifyTotp(code, out timeStepMatched, window);

            return result;
        } // VerifyTOTP
    }
}
