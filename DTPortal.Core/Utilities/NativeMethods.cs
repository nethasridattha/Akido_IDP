using System;
using System.Runtime.InteropServices;

namespace DTPortal.Core.Utilities
{
    internal static class NativeMethods
    {
#if Linux
        private const string DLL_PATH = "libPKIServiceNativeUtils.so";
        private const string PKI_SERVICE_DLL_PATH = "libPKIService.so";
#elif Windows
        //private const string DLL_PATH = "PKIServiceNativeUtils.dll";
        //private const string PKI_SERVICE_DLL_PATH = "PKIService.dll";
        private const string PKI_SERVICE_DLL_PATH = "libPKIService.so";
        private const string DLL_PATH = "libPKIServiceNativeUtils.so";
#endif

        [DllImport(DLL_PATH,
            BestFitMapping = false, CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, EntryPoint = "InitializeNativeUtils",
            ThrowOnUnmappableChar = true)]
        internal static extern int InitializePKINative();

        [DllImport(DLL_PATH,
           BestFitMapping = false, CallingConvention = CallingConvention.Cdecl,
           CharSet = CharSet.Ansi, EntryPoint = "PKIAddChecksumToTransaction",
           ThrowOnUnmappableChar = true)]
        internal static extern int AddChecksumNative(
           [MarshalAs(UnmanagedType.LPStr)] string data,
           int dataLength,
           ref IntPtr checksum,
           ref int checksumLength);

        [DllImport(DLL_PATH,
           BestFitMapping = false, CallingConvention = CallingConvention.Cdecl,
           CharSet = CharSet.Ansi, EntryPoint = "PKIVerifyChecksumOfTransaction",
           ThrowOnUnmappableChar = true)]
        internal static extern int VerifyChecksumNative(
           [MarshalAs(UnmanagedType.LPStr)] string data,
           int dataLength);

        [DllImport(DLL_PATH,
           BestFitMapping = false, CallingConvention = CallingConvention.Cdecl,
           CharSet = CharSet.Ansi, EntryPoint = "PKIWrapSecureData",
           ThrowOnUnmappableChar = true)]
        internal static extern int PKIWrapSecureDataNative(
           [MarshalAs(UnmanagedType.LPStr)] string plainData,
           int dataLength,
           int fileFormatVersion,
           int fileContentVersion,
           int configType,
           ref IntPtr wrappedData,
           ref int wrappedDataLength);

        [DllImport(DLL_PATH,
            BestFitMapping = false, CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, EntryPoint = "CleanupNativeUtils",
            ThrowOnUnmappableChar = true)]
        internal static extern int CleanupPKINative();

        [DllImport(DLL_PATH,
            BestFitMapping = false, CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, EntryPoint = "PKINativeUtilsGetStatusMessage",
            ThrowOnUnmappableChar = true)]
        internal static extern IntPtr GetStatusMessagePKINative(int errorCode);

        [DllImport(DLL_PATH,
            BestFitMapping = false, CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, EntryPoint = "PKIFreeMemory",
            ThrowOnUnmappableChar = true)]
        internal static extern int FreeMemoryPKINative(
            IntPtr buffer);


        [DllImport(DLL_PATH,
           BestFitMapping = false, CallingConvention = CallingConvention.Cdecl,
           CharSet = CharSet.Ansi, EntryPoint = "PKICreateSecureWireData",
           ThrowOnUnmappableChar = true)]
         internal static extern int PKICreateSecureWireDataNative(
           [MarshalAs(UnmanagedType.LPStr)] string plainData,
           int dataLength,
           ref IntPtr SecureWireData,
           ref int SecureWireDataLength);

        [DllImport(DLL_PATH,
           BestFitMapping = false, CallingConvention = CallingConvention.Cdecl,
           CharSet = CharSet.Ansi, EntryPoint = "PKIDecryptSecureWireData",
           ThrowOnUnmappableChar = true)]
         internal static extern int PKIDecryptSecureWireDataNative(
           [MarshalAs(UnmanagedType.LPStr)] string secureWireData,
           int secureWireDataLength,
           ref IntPtr plainData,
           ref int plainDataLength);

        [DllImport(PKI_SERVICE_DLL_PATH,
            BestFitMapping = false, CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, EntryPoint = "GenerateTimestampEx",
            ThrowOnUnmappableChar = true)]
        internal static extern int GenerateTimestampNative(
            [MarshalAs(UnmanagedType.LPArray)] byte[] timestampRequest,
            int timestampRequestLength,
            ref IntPtr response,
            ref int responseLength);

        [DllImport(PKI_SERVICE_DLL_PATH,
            BestFitMapping = false, CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, EntryPoint = "GenerateTimestamp",
            ThrowOnUnmappableChar = true)]
        internal static extern int POSDigiTimeStampNative(
            [MarshalAs(UnmanagedType.LPArray)] byte[] timestampRequest,
            int timestampRequestLength,
            ref IntPtr response,
            ref int responseLength);

        [DllImport(DLL_PATH,
           BestFitMapping = false, CallingConvention = CallingConvention.Cdecl,
           CharSet = CharSet.Ansi, EntryPoint = "PKIValidateAndGetLicenseData",
           ThrowOnUnmappableChar = true)]
        internal static extern int PKIValidateAndGetLicenseData(
           [MarshalAs(UnmanagedType.LPStr)] string path,
           ref int numberOfSubscribers,
           ref int numberOfCertificates,
           ref int numberOfOnboardings);

        [DllImport(DLL_PATH,
           BestFitMapping = false, CallingConvention = CallingConvention.Cdecl,
           CharSet = CharSet.Ansi, EntryPoint = "PKIGenerateSHA256Hash",
           ThrowOnUnmappableChar = true)]
        internal static extern int PKIGenerateSHA256Hash(
           [MarshalAs(UnmanagedType.LPStr)] string path,
           int datalength,
           ref IntPtr encryptedstring,
           ref int encryptedstringlength);
    }
}
