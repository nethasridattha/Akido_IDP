using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Constants
{
    public class FaceVerificationConstants
    {
        public const int SUCCESS = 1;

        public const int ERROR = 2;

        public const int CANCELLED = 3;

        public const int DOCUMENT_VERIFICATION_FAILED = 4;

        public const int FACE_VERIFICATION_FAILED = 5;

        public const int EXPIRED = 6;

        public const int REJECT = -1;

        public const string FACE_VERIFICATION_SUCCESS_MESSAGE = "Face verification successful.";

        public const string FACE_VERIFICATION_ERROR_MESSAGE = "An error occurred during face verification.";

        public const string FACE_VERIFICATION_CANCELLED_MESSAGE = "Face verification was cancelled by the user.";

        public const string DOCUMENT_VERIFICATION_FAILED_MESSAGE = "Document verification failed.";

        public const string FACE_VERIFICATION_FAILED_MESSAGE = "Face verification failed.";

        public const string EXPIRED_MESSAGE = "The face verification session has expired. Please try again.";
    }
}
