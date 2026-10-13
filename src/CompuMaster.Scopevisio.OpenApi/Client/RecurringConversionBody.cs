using CompuMaster.Scopevisio.OpenApi.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace CompuMaster.Scopevisio.OpenApi.Client
{
    internal static class RecurringConversionBody
    {
        internal static string Snapshot(RecurringInvoiceConversionRequest request)
        {
            if (request == null) return null;
            var snapshot = (JObject)request.DeepClone();
            foreach (var name in new[] { "validFrom", "validTo", "nextInvoiceDate" })
            {
                var token = snapshot[name];
                if (token == null || token.Type != JTokenType.Integer ||
                    !long.TryParse(token.ToString(Newtonsoft.Json.Formatting.None), System.Globalization.NumberStyles.Integer,
                        System.Globalization.CultureInfo.InvariantCulture, out _))
                    throw new ArgumentException("The required " + name + " field must be an Int64 JSON integer.", nameof(request));
            }
            var frequency = snapshot["frequency"];
            if (frequency == null || frequency.Type != JTokenType.String ||
                !new[] { "daily", "weekly", "monthly", "bimonthly", "quarterly", "half-yearly", "yearly" }.Contains(frequency.Value<string>(), StringComparer.Ordinal))
                throw new ArgumentException("An explicit declared recurring-invoice frequency is required.", nameof(request));
            foreach (var value in snapshot.DescendantsAndSelf().OfType<JValue>())
                if ((value.Value is double d && (double.IsNaN(d) || double.IsInfinity(d))) ||
                    (value.Value is float f && (float.IsNaN(f) || float.IsInfinity(f))))
                    throw new ArgumentException("JSON numbers must be finite.", nameof(request));
            return snapshot.ToString(Newtonsoft.Json.Formatting.None);
        }
    }
}
