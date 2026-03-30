using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Utilities
{
    public static class SanitizeForLog
    {
        public static string SanitizeForLogging(this string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;


            return input
                .Replace("\r", "")
                .Replace("\n", "")
                .Replace("\t", " "); 
        }
        public static string SanitizeForLogging(this object input)
        {
            if (input == null) return string.Empty;

            // Convert the object to a string first, then use the string sanitizer
            return input.ToString().SanitizeForLogging();
        }
    }

}
