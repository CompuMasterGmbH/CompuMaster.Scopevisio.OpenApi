using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.Api
{
    public partial class AccountingApi
    {
        /// <summary>Creates an explicitly supplied additional expense position for a travel entry.</summary>
        /// <param name="request">The required complete JSON body with an Int64 travel identity and receipt object array, copied before HTTP I/O.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 201 without a fixed success schema.</returns>
        /// <remarks>The server enforces travel/profile/catalog/rate/file semantics. The documented profile is Reisekostenerfassung Belege (Anzeigen); HTTP 404 ambiguously covers a missing travel entry or authorization. No reimbursement, catalog choice, file conversion or permission inference is added. The overnight/vehicle operations share a manufacturer schema describing vehicle choices; this does not establish overnight-specific values. No new creation retry is added. Cancellation after sending cannot establish the server outcome. Parsed fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentNullException">The request is null.</exception>
        /// <exception cref="ArgumentException">A required value is absent or a known field/receipt has an incompatible JSON kind or integer width.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public JToken CreateTravelExtraPosition(TravelPositionCreationRequest request)
        {
            return CreateTravelExtraPositionWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="CreateTravelExtraPosition"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete creation result.</returns>
        public ApiResponse<JToken> CreateTravelExtraPositionWithHttpInfo(TravelPositionCreationRequest request)
        {
            var body = SnapshotTravelPosition(request, false);
            var localVarPath = "/travelentry/position/extra/new";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("CreateTravelExtraPosition", response, ExceptionFactory);
        }

        /// <inheritdoc cref="CreateTravelExtraPosition"/>
        public Task<JToken> CreateTravelExtraPositionAsync(TravelPositionCreationRequest request)
        {
            return CreateTravelExtraPositionAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreateTravelExtraPosition"/>
        /// <param name="request">The required complete JSON body with an Int64 travel identity and receipt object array, copied before HTTP I/O.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have created the position.</exception>
        public async Task<JToken> CreateTravelExtraPositionAsync(TravelPositionCreationRequest request, CancellationToken cancellationToken)
        {
            return (await CreateTravelExtraPositionAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="CreateTravelExtraPositionWithHttpInfo"/>
        public Task<ApiResponse<JToken>> CreateTravelExtraPositionAsyncWithHttpInfo(TravelPositionCreationRequest request)
        {
            return CreateTravelExtraPositionAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreateTravelExtraPositionWithHttpInfo"/>
        /// <param name="request">The required complete JSON body with an Int64 travel identity and receipt object array, copied before HTTP I/O.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have created the position.</exception>
        public async Task<ApiResponse<JToken>> CreateTravelExtraPositionAsyncWithHttpInfo(TravelPositionCreationRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = SnapshotTravelPosition(request, false);
            var localVarPath = "/travelentry/position/extra/new";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("CreateTravelExtraPosition", response, ExceptionFactory);
        }

        /// <summary>Creates an explicitly supplied overnight position for a travel entry.</summary>
        /// <param name="request">The required complete JSON body with an Int64 travel identity and receipt object array, copied before HTTP I/O.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 201 without a fixed success schema.</returns>
        /// <remarks>The server enforces travel/profile/catalog/rate/file semantics. The documented profile is Reisekostenerfassung Belege (Anzeigen); HTTP 404 ambiguously covers a missing travel entry or authorization. No reimbursement, catalog choice, file conversion or permission inference is added. The overnight/vehicle operations share a manufacturer schema describing vehicle choices; this does not establish overnight-specific values. No new creation retry is added. Cancellation after sending cannot establish the server outcome. Parsed fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentNullException">The request is null.</exception>
        /// <exception cref="ArgumentException">A required value is absent or a known field/receipt has an incompatible JSON kind or integer width.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public JToken CreateTravelOvernightPosition(TravelDistancePositionCreationRequest request)
        {
            return CreateTravelOvernightPositionWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="CreateTravelOvernightPosition"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete creation result.</returns>
        public ApiResponse<JToken> CreateTravelOvernightPositionWithHttpInfo(TravelDistancePositionCreationRequest request)
        {
            var body = SnapshotTravelPosition(request, true);
            var localVarPath = "/travelentry/position/overnight/new";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("CreateTravelOvernightPosition", response, ExceptionFactory);
        }

        /// <inheritdoc cref="CreateTravelOvernightPosition"/>
        public Task<JToken> CreateTravelOvernightPositionAsync(TravelDistancePositionCreationRequest request)
        {
            return CreateTravelOvernightPositionAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreateTravelOvernightPosition"/>
        /// <param name="request">The required complete JSON body with an Int64 travel identity and receipt object array, copied before HTTP I/O.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have created the position.</exception>
        public async Task<JToken> CreateTravelOvernightPositionAsync(TravelDistancePositionCreationRequest request, CancellationToken cancellationToken)
        {
            return (await CreateTravelOvernightPositionAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="CreateTravelOvernightPositionWithHttpInfo"/>
        public Task<ApiResponse<JToken>> CreateTravelOvernightPositionAsyncWithHttpInfo(TravelDistancePositionCreationRequest request)
        {
            return CreateTravelOvernightPositionAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreateTravelOvernightPositionWithHttpInfo"/>
        /// <param name="request">The required complete JSON body with an Int64 travel identity and receipt object array, copied before HTTP I/O.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have created the position.</exception>
        public async Task<ApiResponse<JToken>> CreateTravelOvernightPositionAsyncWithHttpInfo(TravelDistancePositionCreationRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = SnapshotTravelPosition(request, true);
            var localVarPath = "/travelentry/position/overnight/new";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("CreateTravelOvernightPosition", response, ExceptionFactory);
        }

        /// <summary>Creates an explicitly supplied vehicle position for a travel entry.</summary>
        /// <param name="request">The required complete JSON body with an Int64 travel identity and receipt object array, copied before HTTP I/O.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 201 without a fixed success schema.</returns>
        /// <remarks>The server enforces travel/profile/catalog/rate/file semantics. The documented profile is Reisekostenerfassung Belege (Anzeigen); HTTP 404 ambiguously covers a missing travel entry or authorization. No reimbursement, catalog choice, file conversion or permission inference is added. The overnight/vehicle operations share a manufacturer schema describing vehicle choices; this does not establish overnight-specific values. No new creation retry is added. Cancellation after sending cannot establish the server outcome. Parsed fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentNullException">The request is null.</exception>
        /// <exception cref="ArgumentException">A required value is absent or a known field/receipt has an incompatible JSON kind or integer width.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public JToken CreateTravelVehiclePosition(TravelDistancePositionCreationRequest request)
        {
            return CreateTravelVehiclePositionWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="CreateTravelVehiclePosition"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete creation result.</returns>
        public ApiResponse<JToken> CreateTravelVehiclePositionWithHttpInfo(TravelDistancePositionCreationRequest request)
        {
            var body = SnapshotTravelPosition(request, true);
            var localVarPath = "/travelentry/position/vehicle/new";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("CreateTravelVehiclePosition", response, ExceptionFactory);
        }

        /// <inheritdoc cref="CreateTravelVehiclePosition"/>
        public Task<JToken> CreateTravelVehiclePositionAsync(TravelDistancePositionCreationRequest request)
        {
            return CreateTravelVehiclePositionAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreateTravelVehiclePosition"/>
        /// <param name="request">The required complete JSON body with an Int64 travel identity and receipt object array, copied before HTTP I/O.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have created the position.</exception>
        public async Task<JToken> CreateTravelVehiclePositionAsync(TravelDistancePositionCreationRequest request, CancellationToken cancellationToken)
        {
            return (await CreateTravelVehiclePositionAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="CreateTravelVehiclePositionWithHttpInfo"/>
        public Task<ApiResponse<JToken>> CreateTravelVehiclePositionAsyncWithHttpInfo(TravelDistancePositionCreationRequest request)
        {
            return CreateTravelVehiclePositionAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreateTravelVehiclePositionWithHttpInfo"/>
        /// <param name="request">The required complete JSON body with an Int64 travel identity and receipt object array, copied before HTTP I/O.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have created the position.</exception>
        public async Task<ApiResponse<JToken>> CreateTravelVehiclePositionAsyncWithHttpInfo(TravelDistancePositionCreationRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = SnapshotTravelPosition(request, true);
            var localVarPath = "/travelentry/position/vehicle/new";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("CreateTravelVehiclePosition", response, ExceptionFactory);
        }

        private static string SnapshotTravelPosition(JObject request, bool distance)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            var snapshot = (JObject)request.DeepClone();
            var id = snapshot["travelEntryId"];
            long identifier;
            if (id == null || id.Type != JTokenType.Integer || !long.TryParse(id.ToString(Newtonsoft.Json.Formatting.None), NumberStyles.Integer, CultureInfo.InvariantCulture, out identifier))
                throw new ArgumentException("The travelEntryId field is required and must be an Int64 JSON integer.", nameof(request));
            ValidateTravelPositionString(snapshot["typeDisplay"], "typeDisplay", false);
            foreach (var name in distance ? new[] { "amount", "taxRate", "distance" } : new[] { "amount", "taxRate" })
            {
                var value = snapshot[name];
                if (value != null && value.Type != JTokenType.Null && ((value.Type != JTokenType.Integer && value.Type != JTokenType.Float) ||
                    !System.Text.RegularExpressions.Regex.IsMatch(value.ToString(Newtonsoft.Json.Formatting.None), @"^-?(?:0|[1-9]\d*)(?:\.\d+)?(?:[eE][+-]?\d+)?$")))
                    throw new ArgumentException("The " + name + " field must be a finite JSON number or null.", nameof(request));
            }
            if (!(snapshot["fileform"] is JArray receipts)) throw new ArgumentException("The required fileform field must be a JSON array.", nameof(request));
            foreach (var receipt in receipts)
            {
                if (!(receipt is JObject file)) throw new ArgumentException("Receipt entries must be JSON objects.", nameof(request));
                ValidateTravelPositionString(file["data"], "receipt data", true);
                ValidateTravelPositionString(file["filename"], "receipt filename", false);
            }
            return snapshot.ToString(Newtonsoft.Json.Formatting.None);
        }

        private static void ValidateTravelPositionString(JToken token, string name, bool required)
        {
            if (!required && (token == null || token.Type == JTokenType.Null)) return;
            if (token == null || token.Type != JTokenType.String)
                throw new ArgumentException("The " + name + " field must be a JSON string.", "request");
        }
    }
}
