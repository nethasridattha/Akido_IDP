using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Utilities
{
    class PKINativeMethods
    {
#if Linux
        private const string PKI_SERVICE_DLL_PATH = "libPKIService.so";
#elif Windows
       //private const string PKI_SERVICE_DLL_PATH = "PKIService.dll";
       private const string PKI_SERVICE_DLL_PATH = "libPKIService.so";
#endif
        [DllImport(PKI_SERVICE_DLL_PATH,
            BestFitMapping = false, CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, EntryPoint = "InitializePKI",
            ThrowOnUnmappableChar = true)]
        internal static extern int InitializePKIServiceNative(
            [MarshalAs(UnmanagedType.LPStr)] string data,
            IntPtr buffer);

        [DllImport(PKI_SERVICE_DLL_PATH,
            BestFitMapping = false, CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, EntryPoint = "CleanupPKI",
            ThrowOnUnmappableChar = true)]
        internal static extern int CleanupPKIServiceNative();

        [DllImport(PKI_SERVICE_DLL_PATH,
            BestFitMapping = false, CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, EntryPoint = "GetStatusMessagePKI",
            ThrowOnUnmappableChar = true)]
        internal static extern IntPtr GetStatusMsgPKIServiceNative(int errorCode);

        [DllImport(PKI_SERVICE_DLL_PATH,
            BestFitMapping = false, CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, EntryPoint = "FreeMemoryPKI",
            ThrowOnUnmappableChar = true)]
        internal static extern int FreeMemoryPKIServiceNative(
            IntPtr buffer);

        [DllImport(PKI_SERVICE_DLL_PATH,
           BestFitMapping = false, CallingConvention = CallingConvention.Cdecl,
           CharSet = CharSet.Ansi, EntryPoint = "IssueCertificate",
           ThrowOnUnmappableChar = true)]
        internal static extern int IssueCertificateNative(
           [MarshalAs(UnmanagedType.LPStr)] string data,
           ref IntPtr response,
           ref int responseLength);

        [DllImport(PKI_SERVICE_DLL_PATH,
           BestFitMapping = false, CallingConvention = CallingConvention.Cdecl,
           CharSet = CharSet.Ansi, EntryPoint = "GenerateSignature",
           ThrowOnUnmappableChar = true)]
        internal static extern int GenerateSignatureNative(
           [MarshalAs(UnmanagedType.LPStr)] string data,
           ref IntPtr response,
           ref int responseLength);
    }
}
