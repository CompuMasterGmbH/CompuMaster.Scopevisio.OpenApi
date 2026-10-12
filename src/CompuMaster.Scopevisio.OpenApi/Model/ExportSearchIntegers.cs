using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Globalization;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    internal static class ExportSearchIntegers
    {
        internal static long? Read(JObject query, string name)
        {
            var token = query[name];
            if (token == null || token.Type == JTokenType.Null) return null;
            if (token.Type != JTokenType.Integer || !long.TryParse(token.ToString(Formatting.None), NumberStyles.Integer, CultureInfo.InvariantCulture, out var value))
                throw new ArgumentException("An export cursor or bound must be an Int64 JSON integer or null.", "query");
            return value;
        }

        internal static void Set(JObject query, string name, long? value)
        {
            if (value.HasValue) query[name] = value.Value;
            else query.Remove(name);
        }
    }
}
