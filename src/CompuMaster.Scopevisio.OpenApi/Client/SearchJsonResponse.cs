using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

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
            var headers = response.Headers == null ? new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase) :
                response.Headers.GroupBy(header => header.Name, StringComparer.OrdinalIgnoreCase)
                    .ToDictionary(group => group.Key, group => string.Join(",", group.Select(header => Convert.ToString(header.Value))),
                        StringComparer.OrdinalIgnoreCase);
            return new ApiResponse<JToken>((int)response.StatusCode, headers, response.Content, data);
        }
    }
}
