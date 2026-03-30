using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace DTPortal.Core.Utilities
{
    public class OperatingSystem
    {
        public string IsWindows()
        {
            if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return "PKINative.dll";
            }
            else
            {
                return default;
            }
        }

        public static bool IsWindowss() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        public static bool IsMacOS() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

        public static bool IsLinux() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
    }
}
