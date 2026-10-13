using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.Api
{
    public partial class HumanResourcesApi
    {
        /// <summary>Retrieves the complete JSON result for an explicitly selected deprecated availability range.</summary>
        /// <param name="request">The required epoch range and optional search object, deeply copied and validated before HTTP admission.</param>
        /// <returns>The complete JSON result, or null for an empty response.</returns>
        /// <remarks>The deprecated route requires Human-Ressourcen Verwaltung (Anzeigen). Required validFrom and validTill remain original Int64 Unix epoch milliseconds; the server considers year/month. Examples do not insert client date defaults. Optional searchSpecification permits an object or JSON null; the server excludes month, monthRevenue and id from search, without client filter rewriting. HTTP 200 declares no response schema. The modern GetHumanResourceAvailability v2 workflow is separate and uses caller-selected month values. No automatic paging or retry occurs. Parsed fractions use bounded decimal precision; raw JSON remains unchanged.</remarks>
        /// <exception cref="ArgumentNullException">The request is null.</exception>
        /// <exception cref="ArgumentException">A required timestamp has an invalid kind or width, the search value is neither an object nor null, or a number is nonfinite.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the HTTP response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fraction exceeds the decimal range.</exception>
        [Obsolete("Use GetHumanResourceAvailability for the modern v2 workflow with explicitly selected month values.")]
        public JToken SearchHumanResourceAvailabilityLegacy(HumanResourceAvailabilityLegacyRequest request)
        {
            return SearchHumanResourceAvailabilityLegacyWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="SearchHumanResourceAvailabilityLegacy"/>
        /// <returns>The status, headers, unchanged raw response and parsed JSON result.</returns>
        [Obsolete("Use GetHumanResourceAvailability for the modern v2 workflow with explicitly selected month values.")]
        public ApiResponse<JToken> SearchHumanResourceAvailabilityLegacyWithHttpInfo(HumanResourceAvailabilityLegacyRequest request)
        {
            var localVarPath = "/humanresourcesAvailability";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), SnapshotLegacyAvailability(request),
                SearchJsonResponse.Headers(Configuration), new Dictionary<string, string>(),
                new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("SearchHumanResourceAvailabilityLegacy", response, ExceptionFactory);
        }

        /// <inheritdoc cref="SearchHumanResourceAvailabilityLegacy"/>
        [Obsolete("Use GetHumanResourceAvailability for the modern v2 workflow with explicitly selected month values.")]
        public Task<JToken> SearchHumanResourceAvailabilityLegacyAsync(HumanResourceAvailabilityLegacyRequest request)
        {
            return SearchHumanResourceAvailabilityLegacyAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchHumanResourceAvailabilityLegacy"/>
        /// <param name="request">The required epoch range and optional search object, deeply copied and validated before HTTP admission.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        [Obsolete("Use GetHumanResourceAvailability for the modern v2 workflow with explicitly selected month values.")]
        public async Task<JToken> SearchHumanResourceAvailabilityLegacyAsync(HumanResourceAvailabilityLegacyRequest request, CancellationToken cancellationToken)
        {
            return (await SearchHumanResourceAvailabilityLegacyAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="SearchHumanResourceAvailabilityLegacyWithHttpInfo"/>
        [Obsolete("Use GetHumanResourceAvailability for the modern v2 workflow with explicitly selected month values.")]
        public Task<ApiResponse<JToken>> SearchHumanResourceAvailabilityLegacyAsyncWithHttpInfo(HumanResourceAvailabilityLegacyRequest request)
        {
            return SearchHumanResourceAvailabilityLegacyAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchHumanResourceAvailabilityLegacyWithHttpInfo"/>
        /// <param name="request">The required epoch range and optional search object, deeply copied and validated before HTTP admission.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        [Obsolete("Use GetHumanResourceAvailability for the modern v2 workflow with explicitly selected month values.")]
        public async Task<ApiResponse<JToken>> SearchHumanResourceAvailabilityLegacyAsyncWithHttpInfo(HumanResourceAvailabilityLegacyRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/humanresourcesAvailability";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), SnapshotLegacyAvailability(request),
                SearchJsonResponse.Headers(Configuration), new Dictionary<string, string>(),
                new Dictionary<string, FileParameter>(), new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("SearchHumanResourceAvailabilityLegacy", response, ExceptionFactory);
        }

        private static string SnapshotLegacyAvailability(HumanResourceAvailabilityLegacyRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            var snapshot = (JObject)request.DeepClone();
            foreach (var name in new[] { "validFrom", "validTill" })
            {
                var token = snapshot[name];
                if (token == null || token.Type != JTokenType.Integer ||
                    !long.TryParse(token.ToString(Newtonsoft.Json.Formatting.None), System.Globalization.NumberStyles.Integer,
                        System.Globalization.CultureInfo.InvariantCulture, out _))
                    throw new ArgumentException("The required " + name + " field must be an Int64 JSON integer.", nameof(request));
            }
            var search = snapshot["searchSpecification"];
            if (search != null && search.Type != JTokenType.Null && !(search is JObject))
                throw new ArgumentException("The optional search specification must be a JSON object or null.", nameof(request));
            foreach (var value in snapshot.DescendantsAndSelf().OfType<JValue>())
            {
                if ((value.Value is double d && (double.IsNaN(d) || double.IsInfinity(d))) ||
                    (value.Value is float f && (float.IsNaN(f) || float.IsInfinity(f))))
                    throw new ArgumentException("JSON numbers must be finite.", nameof(request));
            }
            return snapshot.ToString(Newtonsoft.Json.Formatting.None);
        }
    }
}
