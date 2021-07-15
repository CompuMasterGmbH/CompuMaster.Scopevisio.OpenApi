using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi
{
    internal static class Tools
    {
        /// <summary>
        /// Configuration for accessing OpenScope API
        /// </summary>
        public static string Indent(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return String.Empty;
            else
            {
                string result = "    " + value.Replace("\n", "\n    ");
                if (result.EndsWith("\n    "))
                    result = result.Substring(0, result.Length - ("\n    ".Length - 1));
                return result;
            }
        }
    }
}