using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;

namespace CompuMaster.Scopevisio.OpenApi.Client
{
    // Shared parsing only; HTTP calls remain in the audited API partials.
    internal static class SearchJsonResponse
    {
        internal static Dictionary<string, string> Headers(Configuration configuration)
        {
            var headers = new Dictionary<string, string>(configuration.DefaultHeader, StringComparer.OrdinalIgnoreCase);
            headers["Accept"] = "application/json";
            if (!string.IsNullOrEmpty(configuration.AccessToken))
                headers["Authorization"] = "Bearer " + configuration.AccessToken;
            return headers;
        }

        internal static Dictionary<string, string> ResponseHeaders(RestResponse response)
        {
            var headers = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            if (response.Headers != null)
                foreach (var header in response.Headers)
                    AppendHeader(headers, header.Name, Convert.ToString(header.Value));
            if (response.ContentHeaders != null)
                foreach (var header in response.ContentHeaders)
                    AppendHeader(headers, header.Name, Convert.ToString(header.Value));
            return headers;
        }

        private static void AppendHeader(Dictionary<string, string> headers, string name, string value)
        {
            if (headers.TryGetValue(name, out var previous)) headers[name] = previous + "," + value;
            else headers.Add(name, value);
        }

        internal static ApiResponse<JToken> Parse(string method, RestResponse response, ExceptionFactory exceptionFactory)
        {
            Exception error = exceptionFactory?.Invoke(method, response);
            if (error != null) throw error;

            // These settings are confined to additive search results. Original APIs retain their serializers.
            var data = string.IsNullOrWhiteSpace(response.Content) ? null :
                JsonConvert.DeserializeObject<JToken>(response.Content, new JsonSerializerSettings
                {
                    FloatParseHandling = FloatParseHandling.Decimal,
                    DateParseHandling = DateParseHandling.None
                });
            var headers = ResponseHeaders(response);
            return new ApiResponse<JToken>((int)response.StatusCode, headers, response.Content, data);
        }
    }
}
