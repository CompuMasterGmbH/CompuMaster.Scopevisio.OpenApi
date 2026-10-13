using RestSharp;
using System.Collections.Generic;

namespace CompuMaster.Scopevisio.OpenApi.Client
{
    internal static class RawTextResponse
    {
        internal static Dictionary<string, string> Headers(Configuration configuration)
        {
            var headers = SearchJsonResponse.Headers(configuration);
            headers["Accept"] = "*/*";
            return headers;
        }

        internal static ApiResponse<string> Parse(string method, RestResponse response, ExceptionFactory exceptionFactory)
        {
            var error = exceptionFactory?.Invoke(method, response);
            if (error != null) throw error;
            return new ApiResponse<string>((int)response.StatusCode,
                SearchJsonResponse.ResponseHeaders(response), null, response.Content);
        }
    }
}
