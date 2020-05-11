using System;

namespace CompuMaster.Scopevisio.OpenApi.Client
{
    /// <summary>
    /// Converter for Scopevisio datetime strings
    /// </summary>
    internal class ScopevisioDateTimeConverter 
    {
        /// <summary>
        /// Convert DateTime to Scopevisio datetime string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        static public string ToString(DateTime value)
        {
            return value.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture) + " " + value.ToString("zzz", System.Globalization.CultureInfo.InvariantCulture).Replace(":", "");
        }

        /// <summary>
        /// Convert Scopevisio datetime string to DateTime
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        static public DateTime ToDate(string value)
        {
            return DateTime.Parse(value, System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}
