using System;
using System.Runtime.InteropServices;

using DTPortal.Core.Exceptions;

namespace DTPortal.Core.Utilities
{
    public sealed class PKIMethods
    {
        private static readonly Lazy<PKIMethods> instance = new Lazy<PKIMethods>(() => new PKIMethods());

        private PKIMethods()
        {
            InitializePKI();
        }

        public static PKIMethods Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private void InitializePKI()
        {
            try
            {
                int result = NativeMethods.InitializePKINative();
                if (result != 0)
                {
                    string error = "Failed to initialize PKI Service.";
                    throw new PKIException(error, result);
                }
            }
            catch (PKIException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new PKIException(ex.Message);
            }
        }

        public string AddChecksum(string data)
        {
            // local variables
            string response;
            IntPtr responseBuffer = IntPtr.Zero;
            int responseBufferLength = 0;

            try
            {
                if (String.IsNullOrEmpty(data))
                {
                    throw new PKIException("Input data must not be null or empty");
                }

                int result = NativeMethods.AddChecksumNative(
                     data,
                     data.Length,
                     ref responseBuffer,
                     ref responseBufferLength);
                if (result != 0)
                {
                    string error = GetStatusMessagePKI(result);
                    throw new PKIException(error, result);
                }

                response = Marshal.PtrToStringAnsi(responseBuffer, responseBufferLength);

                return response;
            }
            catch (PKIException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new PKIException(ex.Message);
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
        }

        public bool VerifyChecksum(string data)
        {
            try
            {
                if (String.IsNullOrEmpty(data))
                {
                    throw new PKIException("Input data must not be null or empty");
                }

                int result = NativeMethods.VerifyChecksumNative(
                     data,
                     data.Length);
                if (result != 0)
                {
                    string error = GetStatusMessagePKI(result);
                    //throw new PKIException(error, result);
                    return false;
                }

                return true;
            }
            catch (PKIException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new PKIException(ex.Message);
            }
        }

        public string PKIWrapSecureData(string data)
        {
            // local variables
            string response;
            IntPtr responseBuffer = IntPtr.Zero;
            int responseBufferLength = 0;
            int fileFormatVersion = 1;
            int fileContentVersion = 1;
            int configType = 1;

            try
            {
                if (String.IsNullOrEmpty(data))
                {
                    throw new PKIException("Input data must not be null or empty");
                }

                int result = NativeMethods.PKIWrapSecureDataNative(
                     data,
                     data.Length,
                     fileFormatVersion,
                     fileContentVersion,
                     configType,
                     ref responseBuffer,
                     ref responseBufferLength);
                if (result != 0)
                {
                    string error = GetStatusMessagePKI(result);
                    throw new PKIException(error, result);
                }

                response = Marshal.PtrToStringAnsi(responseBuffer, responseBufferLength);

                return response;
            }
            catch (PKIException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new PKIException(ex.Message);
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
        }

        private string GetStatusMessagePKI(int errorCode)
        {
            string response = null;
            IntPtr responseBuffer = IntPtr.Zero;

            try
            {
                responseBuffer = NativeMethods.GetStatusMessagePKINative(errorCode);

                response = Marshal.PtrToStringAnsi(responseBuffer);
                return response;
            }
            catch (PKIException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new PKIException(ex.Message);
            }
        }

        private void FreeMemoryPKI(IntPtr buffer)
        {
            try
            {
                int result = NativeMethods.FreeMemoryPKINative(buffer);
                if (result != 0)
                {
                    string error = GetStatusMessagePKI(result);
                    throw new PKIException(error, result);
                }
            }
            catch (Exception ex)
            {
                throw new PKIException(ex.Message);
            }
        }

        private void CleanupPKI()
        {
            try
            {
                int result = NativeMethods.CleanupPKINative();
                if (result != 0)
                {
                    string error = GetStatusMessagePKI(result);
                    throw new PKIException(error, result);
                }
            }
            catch (PKIException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new PKIException(ex.Message);
            }
        }

        public string PKICreateSecureWireData(string data)
        {
            // local variables
            string response;
            IntPtr responseBuffer = IntPtr.Zero;
            int responseBufferLength = 0;

            try
            {
                int result = NativeMethods.PKICreateSecureWireDataNative(
                     data,
                     data.Length,
                     ref responseBuffer,
                     ref responseBufferLength);
                if (result != 0)
                {
                    string error = GetStatusMessagePKI(result);
                    throw new PKIException(error, result);
                }

                response = Marshal.PtrToStringAnsi(responseBuffer, responseBufferLength);

                return response;
            }
            catch (PKIException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new PKIException(ex.Message);
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
        }

        public string PKIDecryptSecureWireData(string secureWireData)
        {
            // local variables
            string response;
            IntPtr responseBuffer = IntPtr.Zero;
            int responseBufferLength = 0;

            try
            {
                int result = NativeMethods.PKIDecryptSecureWireDataNative(
                     secureWireData,
                     secureWireData.Length,
                     ref responseBuffer,
                     ref responseBufferLength);
                if (result != 0)
                {
                    string error = GetStatusMessagePKI(result);
                    throw new PKIException(error, result);
                }

                response = Marshal.PtrToStringAnsi(responseBuffer, responseBufferLength);

                return response;
            }
            catch (PKIException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new PKIException(ex.Message);
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
        }

        public byte[] GenerateTimestamp(byte[] timestampRequest)
        {
            // local variables
            byte[] response;
            IntPtr responseBuffer = IntPtr.Zero;
            int responseBufferLength = 0;

            try
            {
                if (timestampRequest == null || timestampRequest.Length == 0)
                {
                    throw new PKIException("Input data must not be null or empty");
                }

                int result = NativeMethods.GenerateTimestampNative(
                     timestampRequest,
                     timestampRequest.Length,
                     ref responseBuffer,
                     ref responseBufferLength);
                if (result != 0)
                {
                    string error = GetStatusMessagePKI(result);
                    throw new PKIException(error, result);
                }

                response = new byte[responseBufferLength];
                Marshal.Copy(responseBuffer, response, 0, responseBufferLength);

                return response;
            }
            catch (PKIException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new PKIException(ex.Message);
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
        }

        public byte[] POSDigiTimeStamp(byte[] timestampRequest)
        {
            // local variables
            byte[] response;
            IntPtr responseBuffer = IntPtr.Zero;
            int responseBufferLength = 0;

            try
            {
                if (timestampRequest == null || timestampRequest.Length == 0)
                {
                    throw new PKIException("Input data must not be null or empty");
                }

                int result = NativeMethods.POSDigiTimeStampNative(
                     timestampRequest,
                     timestampRequest.Length,
                     ref responseBuffer,
                     ref responseBufferLength);
                if (result != 0)
                {
                    string error = GetStatusMessagePKI(result);
                    throw new PKIException(error, result);
                }

                response = new byte[responseBufferLength];
                Marshal.Copy(responseBuffer, response, 0, responseBufferLength);

                return response;
            }
            catch (PKIException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new PKIException(ex.Message);
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
        }

        public (int, int, int) PKIGetLicenseData(string path)
        {
            // local variables
            int numberOfSubscribers = 0;
            int numberOfCertificates = 0;
            int numberOfOnboardings = 0;

            try
            {
                int result = NativeMethods.PKIValidateAndGetLicenseData(
                     path,
                     ref numberOfSubscribers,
                     ref numberOfCertificates,
                     ref numberOfOnboardings);
                if (result != 0)
                {
                    string error = GetStatusMessagePKI(result);
                    throw new PKIException(error, result);
                }
                    
                return (numberOfSubscribers, numberOfCertificates, numberOfOnboardings);
            }
            catch (PKIException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new PKIException(ex.Message);
            }
        }
        public string PKIGenerateSHA256Hash(string data)
        {
            // local variables
            string response;
            IntPtr responseBuffer = IntPtr.Zero;
            int responseBufferLength = 0;

            try
            {
                int result = NativeMethods.PKIGenerateSHA256Hash(
                     data,
                     data.Length,
                     ref responseBuffer,
                     ref responseBufferLength);
                if (result != 0)
                {
                    string error = GetStatusMessagePKI(result);
                    throw new PKIException(error, result);
                }

                response = Marshal.PtrToStringAnsi(responseBuffer, responseBufferLength);

                return response;
            }
            catch (PKIException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new PKIException(ex.Message);
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
        }
        ~PKIMethods()
        {
            CleanupPKI();
        }
    }
}
