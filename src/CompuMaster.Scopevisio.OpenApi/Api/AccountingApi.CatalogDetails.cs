using CompuMaster.Scopevisio.OpenApi.Client;
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
        /// <summary>Retrieves the complete JSON accounting information for the active organisation.</summary>
        /// <returns>The complete JSON response, or null for an empty response body.</returns>
        /// <remarks>The manufacturer lists the Stammdaten (Anzeigen) profile and HTTP 500 when accounting is not set up for the organisation. No fixed success schema is declared. Historical generated methods, default data, token/account selection and fiscal configuration remain unchanged. Fractional values use .NET decimal precision and may round or exceed its range; raw JSON remains exact.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken GetAccountingInfoDetails()
        {
            return GetAccountingInfoDetailsWithHttpInfo().Data;
        }

        /// <inheritdoc cref="GetAccountingInfoDetails"/>
        /// <returns>The status, headers, unchanged raw response and complete parsed JSON.</returns>
        public ApiResponse<JToken> GetAccountingInfoDetailsWithHttpInfo()
        {
            var localVarPath = "/accountinginfo";
            var queryParams = new List<KeyValuePair<string, string>>();
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetAccountingInfoDetails", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetAccountingInfoDetails"/>
        public Task<JToken> GetAccountingInfoDetailsAsync()
        {
            return GetAccountingInfoDetailsAsync(CancellationToken.None);
        }

        /// <inheritdoc cref="GetAccountingInfoDetails"/>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<JToken> GetAccountingInfoDetailsAsync(CancellationToken cancellationToken)
        {
            return (await GetAccountingInfoDetailsAsyncWithHttpInfo(cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetAccountingInfoDetailsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetAccountingInfoDetailsAsyncWithHttpInfo()
        {
            return GetAccountingInfoDetailsAsyncWithHttpInfo(CancellationToken.None);
        }

        /// <inheritdoc cref="GetAccountingInfoDetailsWithHttpInfo"/>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetAccountingInfoDetailsAsyncWithHttpInfo(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/accountinginfo";
            var queryParams = new List<KeyValuePair<string, string>>();
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetAccountingInfoDetails", response, ExceptionFactory);
        }

        /// <summary>Retrieves the complete JSON payment terms for the active organisation.</summary>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <returns>The complete JSON response, or null for an empty response body.</returns>
        /// <remarks>The manufacturer lists the Stammdaten, Zahlungsbedingungen (Anzeigen) profiles. Original optional fields retain null omission and explicit empty values. No fixed success schema is declared. Historical generated methods, default data, token/account selection and fiscal configuration remain unchanged. Fractional values use .NET decimal precision and may round or exceed its range; raw JSON remains exact.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken GetPaymentTermsDetails(string fields = null)
        {
            return GetPaymentTermsDetailsWithHttpInfo(fields).Data;
        }

        /// <inheritdoc cref="GetPaymentTermsDetails"/>
        /// <returns>The status, headers, unchanged raw response and complete parsed JSON.</returns>
        public ApiResponse<JToken> GetPaymentTermsDetailsWithHttpInfo(string fields = null)
        {
            var localVarPath = "/paymentterms";
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetPaymentTermsDetails", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetPaymentTermsDetails"/>
        public Task<JToken> GetPaymentTermsDetailsAsync(string fields = null)
        {
            return GetPaymentTermsDetailsAsync(fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetPaymentTermsDetails"/>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<JToken> GetPaymentTermsDetailsAsync(string fields, CancellationToken cancellationToken)
        {
            return (await GetPaymentTermsDetailsAsyncWithHttpInfo(fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetPaymentTermsDetailsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetPaymentTermsDetailsAsyncWithHttpInfo(string fields = null)
        {
            return GetPaymentTermsDetailsAsyncWithHttpInfo(fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetPaymentTermsDetailsWithHttpInfo"/>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetPaymentTermsDetailsAsyncWithHttpInfo(string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/paymentterms";
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetPaymentTermsDetails", response, ExceptionFactory);
        }

        /// <summary>Retrieves the complete JSON details of a selected payment term.</summary>
        /// <param name="id">The explicitly selected payment-term identifier.</param>
        /// <returns>The complete JSON response, or null for an empty response body.</returns>
        /// <remarks>The manufacturer lists the Stammdaten, Zahlungsbedingungen (Anzeigen) profiles and HTTP 404 for a missing payment term. The original signed Int64 identifier is sent unchanged; no minimum is declared. No fixed success schema is declared. Historical generated methods, default data, token/account selection and fiscal configuration remain unchanged. Fractional values use .NET decimal precision and may round or exceed its range; raw JSON remains exact.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken GetPaymentTermDetails(long id)
        {
            return GetPaymentTermDetailsWithHttpInfo(id).Data;
        }

        /// <inheritdoc cref="GetPaymentTermDetails"/>
        /// <returns>The status, headers, unchanged raw response and complete parsed JSON.</returns>
        public ApiResponse<JToken> GetPaymentTermDetailsWithHttpInfo(long id)
        {
            var localVarPath = "/paymentterm/{id}";
            localVarPath = localVarPath.Replace("{id}", id.ToString(CultureInfo.InvariantCulture));
            var queryParams = new List<KeyValuePair<string, string>>();
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetPaymentTermDetails", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetPaymentTermDetails"/>
        public Task<JToken> GetPaymentTermDetailsAsync(long id)
        {
            return GetPaymentTermDetailsAsync(id, CancellationToken.None);
        }

        /// <inheritdoc cref="GetPaymentTermDetails"/>
        /// <param name="id">The explicitly selected payment-term identifier.</param>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<JToken> GetPaymentTermDetailsAsync(long id, CancellationToken cancellationToken)
        {
            return (await GetPaymentTermDetailsAsyncWithHttpInfo(id, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetPaymentTermDetailsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetPaymentTermDetailsAsyncWithHttpInfo(long id)
        {
            return GetPaymentTermDetailsAsyncWithHttpInfo(id, CancellationToken.None);
        }

        /// <inheritdoc cref="GetPaymentTermDetailsWithHttpInfo"/>
        /// <param name="id">The explicitly selected payment-term identifier.</param>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetPaymentTermDetailsAsyncWithHttpInfo(long id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/paymentterm/{id}";
            localVarPath = localVarPath.Replace("{id}", id.ToString(CultureInfo.InvariantCulture));
            var queryParams = new List<KeyValuePair<string, string>>();
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetPaymentTermDetails", response, ExceptionFactory);
        }

        /// <summary>Retrieves the complete JSON text templates.</summary>
        /// <returns>The complete JSON response, or null for an empty response body.</returns>
        /// <remarks>The manufacturer lists the Angebote, Aufträge, Lieferscheine, Rechnungen, Rechnungseingangsbuch (Anzeigen) profiles; actual availability and profile interpretation remain server-authoritative. No fixed success schema is declared. Historical generated methods, default data, token/account selection and fiscal configuration remain unchanged. Fractional values use .NET decimal precision and may round or exceed its range; raw JSON remains exact.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken GetTextTemplatesDetails()
        {
            return GetTextTemplatesDetailsWithHttpInfo().Data;
        }

        /// <inheritdoc cref="GetTextTemplatesDetails"/>
        /// <returns>The status, headers, unchanged raw response and complete parsed JSON.</returns>
        public ApiResponse<JToken> GetTextTemplatesDetailsWithHttpInfo()
        {
            var localVarPath = "/texttemplates";
            var queryParams = new List<KeyValuePair<string, string>>();
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetTextTemplatesDetails", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetTextTemplatesDetails"/>
        public Task<JToken> GetTextTemplatesDetailsAsync()
        {
            return GetTextTemplatesDetailsAsync(CancellationToken.None);
        }

        /// <inheritdoc cref="GetTextTemplatesDetails"/>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<JToken> GetTextTemplatesDetailsAsync(CancellationToken cancellationToken)
        {
            return (await GetTextTemplatesDetailsAsyncWithHttpInfo(cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetTextTemplatesDetailsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetTextTemplatesDetailsAsyncWithHttpInfo()
        {
            return GetTextTemplatesDetailsAsyncWithHttpInfo(CancellationToken.None);
        }

        /// <inheritdoc cref="GetTextTemplatesDetailsWithHttpInfo"/>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetTextTemplatesDetailsAsyncWithHttpInfo(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/texttemplates";
            var queryParams = new List<KeyValuePair<string, string>>();
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetTextTemplatesDetails", response, ExceptionFactory);
        }

        /// <summary>Retrieves the complete JSON VAT-matrix entries for the active organisation.</summary>
        /// <returns>The complete JSON response, or null for an empty response body.</returns>
        /// <remarks>The manufacturer lists the Stammdaten, Steuermatrix (Anzeigen) profiles. No fixed success schema is declared. Historical generated methods, default data, token/account selection and fiscal configuration remain unchanged. Fractional values use .NET decimal precision and may round or exceed its range; raw JSON remains exact.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken GetVatMatrixEntriesDetails()
        {
            return GetVatMatrixEntriesDetailsWithHttpInfo().Data;
        }

        /// <inheritdoc cref="GetVatMatrixEntriesDetails"/>
        /// <returns>The status, headers, unchanged raw response and complete parsed JSON.</returns>
        public ApiResponse<JToken> GetVatMatrixEntriesDetailsWithHttpInfo()
        {
            var localVarPath = "/vatmatrixentries";
            var queryParams = new List<KeyValuePair<string, string>>();
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetVatMatrixEntriesDetails", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetVatMatrixEntriesDetails"/>
        public Task<JToken> GetVatMatrixEntriesDetailsAsync()
        {
            return GetVatMatrixEntriesDetailsAsync(CancellationToken.None);
        }

        /// <inheritdoc cref="GetVatMatrixEntriesDetails"/>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<JToken> GetVatMatrixEntriesDetailsAsync(CancellationToken cancellationToken)
        {
            return (await GetVatMatrixEntriesDetailsAsyncWithHttpInfo(cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetVatMatrixEntriesDetailsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetVatMatrixEntriesDetailsAsyncWithHttpInfo()
        {
            return GetVatMatrixEntriesDetailsAsyncWithHttpInfo(CancellationToken.None);
        }

        /// <inheritdoc cref="GetVatMatrixEntriesDetailsWithHttpInfo"/>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetVatMatrixEntriesDetailsAsyncWithHttpInfo(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/vatmatrixentries";
            var queryParams = new List<KeyValuePair<string, string>>();
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetVatMatrixEntriesDetails", response, ExceptionFactory);
        }

    }
}
