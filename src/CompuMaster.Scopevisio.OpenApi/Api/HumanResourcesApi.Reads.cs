using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.Api
{
    public partial class HumanResourcesApi
    {
        /// <summary>Retrieves typed details of a human resource.</summary>
        /// <param name="id">The required Int64 human resource identifier, sent unchanged.</param>
        /// <param name="fields">The optional selected fields as CSV text; null omits the parameter and an empty string is sent.</param>
        /// <param name="formatValues">The optional formatting choice; null retains the documented server default true, while false and true are sent explicitly.</param>
        /// <returns>The complete typed JSON result, or null for an empty or JSON-null response.</returns>
        /// <remarks>The server enforces profiles, visibility, formatting and range/search semantics. Unknown fields and exact raw responses remain available. Numeric getters follow the unformatted schema; use raw fields when requesting server-formatted text. Fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact. No paging or completeness is inferred.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response has invalid JSON, an incompatible object/record shape or a fraction exceeding the decimal range.</exception>
        public HumanResourceRecord GetHumanResourceDetails(long id, string fields = null, bool? formatValues = null)
        {
            return GetHumanResourceDetailsWithHttpInfo(id, fields, formatValues).Data;
        }

        /// <inheritdoc cref="GetHumanResourceDetails"/>
        /// <returns>The status, headers, exact raw response and typed JSON.</returns>
        public ApiResponse<HumanResourceRecord> GetHumanResourceDetailsWithHttpInfo(long id, string fields = null, bool? formatValues = null)
        {
            var localVarPath = "/humanresource/{id}";
            localVarPath = localVarPath.Replace("{id}", Uri.EscapeDataString(id.ToString(CultureInfo.InvariantCulture)));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            if (formatValues.HasValue) queryParams.Add(new KeyValuePair<string, string>("formatValues", formatValues.Value ? "true" : "false"));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return WrapHumanResourceResponse(SearchJsonResponse.Parse("GetHumanResourceDetails", response, ExceptionFactory), value => new HumanResourceRecord(value));
        }

        /// <inheritdoc cref="GetHumanResourceDetails"/>
        public Task<HumanResourceRecord> GetHumanResourceDetailsAsync(long id, string fields = null, bool? formatValues = null)
        {
            return GetHumanResourceDetailsAsync(id, fields, formatValues, CancellationToken.None);
        }

        /// <inheritdoc cref="GetHumanResourceDetails"/>
        /// <param name="id">The required Int64 human resource identifier, sent unchanged.</param>
        /// <param name="fields">The optional selected fields as CSV text; null omits the parameter and an empty string is sent.</param>
        /// <param name="formatValues">The optional formatting choice; null retains the documented server default true, while false and true are sent explicitly.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<HumanResourceRecord> GetHumanResourceDetailsAsync(long id, string fields, bool? formatValues, CancellationToken cancellationToken)
        {
            return (await GetHumanResourceDetailsAsyncWithHttpInfo(id, fields, formatValues, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetHumanResourceDetailsWithHttpInfo"/>
        public Task<ApiResponse<HumanResourceRecord>> GetHumanResourceDetailsAsyncWithHttpInfo(long id, string fields = null, bool? formatValues = null)
        {
            return GetHumanResourceDetailsAsyncWithHttpInfo(id, fields, formatValues, CancellationToken.None);
        }

        /// <inheritdoc cref="GetHumanResourceDetailsWithHttpInfo"/>
        /// <param name="id">The required Int64 human resource identifier, sent unchanged.</param>
        /// <param name="fields">The optional selected fields as CSV text; null omits the parameter and an empty string is sent.</param>
        /// <param name="formatValues">The optional formatting choice; null retains the documented server default true, while false and true are sent explicitly.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<HumanResourceRecord>> GetHumanResourceDetailsAsyncWithHttpInfo(long id, string fields, bool? formatValues, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/humanresource/{id}";
            localVarPath = localVarPath.Replace("{id}", Uri.EscapeDataString(id.ToString(CultureInfo.InvariantCulture)));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            if (formatValues.HasValue) queryParams.Add(new KeyValuePair<string, string>("formatValues", formatValues.Value ? "true" : "false"));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return WrapHumanResourceResponse(SearchJsonResponse.Parse("GetHumanResourceDetails", response, ExceptionFactory), value => new HumanResourceRecord(value));
        }

        /// <summary>Retrieves typed human resource availability for the selected month bounds.</summary>
        /// <param name="query">The structured availability query with required nonblank JSON month-bound strings, sent unchanged.</param>
        /// <returns>The complete typed JSON result, or null for an empty or JSON-null response.</returns>
        /// <remarks>The server enforces profiles, visibility, formatting and range/search semantics. Unknown fields and exact raw responses remain available. Numeric getters follow the unformatted schema; use raw fields when requesting server-formatted text. Fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact. No paging or completeness is inferred.</remarks>
        /// <exception cref="ArgumentException">The query is null or a required month bound/search specification has an invalid raw JSON kind.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response has invalid JSON, an incompatible object/record shape or a fraction exceeding the decimal range.</exception>
        public HumanResourceAvailabilityResponse GetHumanResourceAvailability(HumanResourceAvailabilityRequest query)
        {
            return GetHumanResourceAvailabilityWithHttpInfo(query).Data;
        }

        /// <inheritdoc cref="GetHumanResourceAvailability"/>
        /// <returns>The status, headers, exact raw response and typed JSON.</returns>
        public ApiResponse<HumanResourceAvailabilityResponse> GetHumanResourceAvailabilityWithHttpInfo(HumanResourceAvailabilityRequest query)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));
            query.ValidateForSend();
            var localVarPath = "/humanresourcesavailability/v2";
            var queryParams = new List<KeyValuePair<string, string>>();
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                queryParams, query.ToString(Newtonsoft.Json.Formatting.None), SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return WrapHumanResourceResponse(SearchJsonResponse.Parse("GetHumanResourceAvailability", response, ExceptionFactory), value => new HumanResourceAvailabilityResponse(value));
        }

        /// <inheritdoc cref="GetHumanResourceAvailability"/>
        public Task<HumanResourceAvailabilityResponse> GetHumanResourceAvailabilityAsync(HumanResourceAvailabilityRequest query)
        {
            return GetHumanResourceAvailabilityAsync(query, CancellationToken.None);
        }

        /// <inheritdoc cref="GetHumanResourceAvailability"/>
        /// <param name="query">The structured availability query with required nonblank JSON month-bound strings, sent unchanged.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<HumanResourceAvailabilityResponse> GetHumanResourceAvailabilityAsync(HumanResourceAvailabilityRequest query, CancellationToken cancellationToken)
        {
            return (await GetHumanResourceAvailabilityAsyncWithHttpInfo(query, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetHumanResourceAvailabilityWithHttpInfo"/>
        public Task<ApiResponse<HumanResourceAvailabilityResponse>> GetHumanResourceAvailabilityAsyncWithHttpInfo(HumanResourceAvailabilityRequest query)
        {
            return GetHumanResourceAvailabilityAsyncWithHttpInfo(query, CancellationToken.None);
        }

        /// <inheritdoc cref="GetHumanResourceAvailabilityWithHttpInfo"/>
        /// <param name="query">The structured availability query with required nonblank JSON month-bound strings, sent unchanged.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<HumanResourceAvailabilityResponse>> GetHumanResourceAvailabilityAsyncWithHttpInfo(HumanResourceAvailabilityRequest query, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (query == null) throw new ArgumentNullException(nameof(query));
            query.ValidateForSend();
            var localVarPath = "/humanresourcesavailability/v2";
            var queryParams = new List<KeyValuePair<string, string>>();
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                queryParams, query.ToString(Newtonsoft.Json.Formatting.None), SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return WrapHumanResourceResponse(SearchJsonResponse.Parse("GetHumanResourceAvailability", response, ExceptionFactory), value => new HumanResourceAvailabilityResponse(value));
        }

        private static ApiResponse<T> WrapHumanResourceResponse<T>(ApiResponse<JToken> response, Func<JObject, T> create) where T : class
        {
            T data = null;
            if (response.Data != null && response.Data.Type != JTokenType.Null)
            {
                if (!(response.Data is JObject value))
                    throw new JsonSerializationException("The human resource response must be a JSON object.");
                data = create(value);
            }
            return new ApiResponse<T>(response.StatusCode, response.Headers, response.RawJsonContent, data);
        }
    }
}
