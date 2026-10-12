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
    public partial class ContactApi
    {
        /// <summary>Retrieves the complete JSON details of a selected contact.</summary>
        /// <param name="id">The explicitly selected contact master identifier.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <param name="formatValues">Whether the server formats values, or null to omit the option and retain its default.</param>
        /// <returns>The complete JSON response, or null for an empty response body.</returns>
        /// <remarks>Omitting formatValues retains the server default of formatted values; explicit false requests unformatted values. The server requires the Kontakte (Anzeigen) profile. A 400 response can mean an unavailable contact or missing read permission. The manufacturer declares no fixed success schema. Existing generated methods and their null-data behavior remain unchanged. Fractional values use .NET decimal precision and may round or exceed its range; raw JSON remains exact.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken GetContactDetails(long id, string fields = null, bool? formatValues = null)
        {
            return GetContactDetailsWithHttpInfo(id, fields, formatValues).Data;
        }

        /// <inheritdoc cref="GetContactDetails"/>
        /// <returns>The status, headers, unchanged raw response and complete parsed JSON.</returns>
        public ApiResponse<JToken> GetContactDetailsWithHttpInfo(long id, string fields = null, bool? formatValues = null)
        {
            var localVarPath = "/contact/{id}";
            localVarPath = localVarPath.Replace("{id}", id.ToString(CultureInfo.InvariantCulture));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            if (formatValues.HasValue) queryParams.Add(new KeyValuePair<string, string>("formatValues", formatValues.Value ? "true" : "false"));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetContactDetails", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetContactDetails"/>
        public Task<JToken> GetContactDetailsAsync(long id, string fields = null, bool? formatValues = null)
        {
            return GetContactDetailsAsync(id, fields, formatValues, CancellationToken.None);
        }

        /// <inheritdoc cref="GetContactDetails"/>
        /// <param name="id">The explicitly selected contact master identifier.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <param name="formatValues">Whether the server formats values, or null to omit the option and retain its default.</param>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<JToken> GetContactDetailsAsync(long id, string fields, bool? formatValues, CancellationToken cancellationToken)
        {
            return (await GetContactDetailsAsyncWithHttpInfo(id, fields, formatValues, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetContactDetailsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetContactDetailsAsyncWithHttpInfo(long id, string fields = null, bool? formatValues = null)
        {
            return GetContactDetailsAsyncWithHttpInfo(id, fields, formatValues, CancellationToken.None);
        }

        /// <inheritdoc cref="GetContactDetailsWithHttpInfo"/>
        /// <param name="id">The explicitly selected contact master identifier.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <param name="formatValues">Whether the server formats values, or null to omit the option and retain its default.</param>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetContactDetailsAsyncWithHttpInfo(long id, string fields, bool? formatValues, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/contact/{id}";
            localVarPath = localVarPath.Replace("{id}", id.ToString(CultureInfo.InvariantCulture));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            if (formatValues.HasValue) queryParams.Add(new KeyValuePair<string, string>("formatValues", formatValues.Value ? "true" : "false"));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetContactDetails", response, ExceptionFactory);
        }

        /// <summary>Retrieves the complete JSON details of a contact selected by an explicit key.</summary>
        /// <param name="keyIdentifier">The explicit key selector; the current server documents ID and LEGACYNUMBER. No default is selected by the client.</param>
        /// <param name="id">The original contact identifier or legacy number, escaped as one path segment without trimming or numeric conversion.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <returns>The complete JSON response, or null for an empty response body.</returns>
        /// <remarks>The server requires the Kontakte (Anzeigen) profile. The vendor prose describes 404 for ambiguous legacy numbers while its response table lists 400 for an unavailable or unreadable contact; original failures are retained. SearchContacts is available when the key does not identify a distinct contact. The manufacturer declares no fixed success schema. Existing generated methods and their null-data behavior remain unchanged. Fractional values use .NET decimal precision and may round or exceed its range; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentException">A string selector is null, blank or a dot path segment.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken GetContactDetailsByKeyIdentifier(string keyIdentifier, string id, string fields = null)
        {
            return GetContactDetailsByKeyIdentifierWithHttpInfo(keyIdentifier, id, fields).Data;
        }

        /// <inheritdoc cref="GetContactDetailsByKeyIdentifier"/>
        /// <returns>The status, headers, unchanged raw response and complete parsed JSON.</returns>
        public ApiResponse<JToken> GetContactDetailsByKeyIdentifierWithHttpInfo(string keyIdentifier, string id, string fields = null)
        {
            ValidateContactReadSelector(keyIdentifier, nameof(keyIdentifier));
            ValidateContactReadSelector(id, nameof(id));
            var localVarPath = "/contact/{keyIdentifier}/{id}";
            localVarPath = localVarPath.Replace("{keyIdentifier}", Uri.EscapeDataString(keyIdentifier));
            localVarPath = localVarPath.Replace("{id}", Uri.EscapeDataString(id));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetContactDetailsByKeyIdentifier", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetContactDetailsByKeyIdentifier"/>
        public Task<JToken> GetContactDetailsByKeyIdentifierAsync(string keyIdentifier, string id, string fields = null)
        {
            return GetContactDetailsByKeyIdentifierAsync(keyIdentifier, id, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetContactDetailsByKeyIdentifier"/>
        /// <param name="keyIdentifier">The explicit key selector; the current server documents ID and LEGACYNUMBER. No default is selected by the client.</param>
        /// <param name="id">The original contact identifier or legacy number, escaped as one path segment without trimming or numeric conversion.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<JToken> GetContactDetailsByKeyIdentifierAsync(string keyIdentifier, string id, string fields, CancellationToken cancellationToken)
        {
            return (await GetContactDetailsByKeyIdentifierAsyncWithHttpInfo(keyIdentifier, id, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetContactDetailsByKeyIdentifierWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetContactDetailsByKeyIdentifierAsyncWithHttpInfo(string keyIdentifier, string id, string fields = null)
        {
            return GetContactDetailsByKeyIdentifierAsyncWithHttpInfo(keyIdentifier, id, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetContactDetailsByKeyIdentifierWithHttpInfo"/>
        /// <param name="keyIdentifier">The explicit key selector; the current server documents ID and LEGACYNUMBER. No default is selected by the client.</param>
        /// <param name="id">The original contact identifier or legacy number, escaped as one path segment without trimming or numeric conversion.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetContactDetailsByKeyIdentifierAsyncWithHttpInfo(string keyIdentifier, string id, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateContactReadSelector(keyIdentifier, nameof(keyIdentifier));
            ValidateContactReadSelector(id, nameof(id));
            var localVarPath = "/contact/{keyIdentifier}/{id}";
            localVarPath = localVarPath.Replace("{keyIdentifier}", Uri.EscapeDataString(keyIdentifier));
            localVarPath = localVarPath.Replace("{id}", Uri.EscapeDataString(id));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetContactDetailsByKeyIdentifier", response, ExceptionFactory);
        }

        /// <summary>Retrieves the complete JSON employee relationships of a selected organisation contact.</summary>
        /// <param name="id">The explicitly selected organisation contact master identifier.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <returns>The complete JSON response, or null for an empty response body.</returns>
        /// <remarks>The server requires the Kontakte (Anzeigen) profile. A 400 response can mean an unavailable contact or missing read permission. No relationship or permission is inferred locally. The manufacturer declares no fixed success schema. Existing generated methods and their null-data behavior remain unchanged. Fractional values use .NET decimal precision and may round or exceed its range; raw JSON remains exact.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken GetContactEmployeeDetails(long id, string fields = null)
        {
            return GetContactEmployeeDetailsWithHttpInfo(id, fields).Data;
        }

        /// <inheritdoc cref="GetContactEmployeeDetails"/>
        /// <returns>The status, headers, unchanged raw response and complete parsed JSON.</returns>
        public ApiResponse<JToken> GetContactEmployeeDetailsWithHttpInfo(long id, string fields = null)
        {
            var localVarPath = "/contact/{id}/employees";
            localVarPath = localVarPath.Replace("{id}", id.ToString(CultureInfo.InvariantCulture));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetContactEmployeeDetails", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetContactEmployeeDetails"/>
        public Task<JToken> GetContactEmployeeDetailsAsync(long id, string fields = null)
        {
            return GetContactEmployeeDetailsAsync(id, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetContactEmployeeDetails"/>
        /// <param name="id">The explicitly selected organisation contact master identifier.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<JToken> GetContactEmployeeDetailsAsync(long id, string fields, CancellationToken cancellationToken)
        {
            return (await GetContactEmployeeDetailsAsyncWithHttpInfo(id, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetContactEmployeeDetailsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetContactEmployeeDetailsAsyncWithHttpInfo(long id, string fields = null)
        {
            return GetContactEmployeeDetailsAsyncWithHttpInfo(id, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetContactEmployeeDetailsWithHttpInfo"/>
        /// <param name="id">The explicitly selected organisation contact master identifier.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetContactEmployeeDetailsAsyncWithHttpInfo(long id, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/contact/{id}/employees";
            localVarPath = localVarPath.Replace("{id}", id.ToString(CultureInfo.InvariantCulture));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetContactEmployeeDetails", response, ExceptionFactory);
        }

        /// <summary>Retrieves the complete JSON employer relationships of a selected personal contact.</summary>
        /// <param name="id">The explicitly selected personal contact master identifier.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <returns>The complete JSON response, or null for an empty response body.</returns>
        /// <remarks>The server requires the Kontakte (Anzeigen) profile. A 400 response can mean an unavailable contact or missing read permission. No relationship or permission is inferred locally. The manufacturer declares no fixed success schema. Existing generated methods and their null-data behavior remain unchanged. Fractional values use .NET decimal precision and may round or exceed its range; raw JSON remains exact.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken GetContactEmployerDetails(long id, string fields = null)
        {
            return GetContactEmployerDetailsWithHttpInfo(id, fields).Data;
        }

        /// <inheritdoc cref="GetContactEmployerDetails"/>
        /// <returns>The status, headers, unchanged raw response and complete parsed JSON.</returns>
        public ApiResponse<JToken> GetContactEmployerDetailsWithHttpInfo(long id, string fields = null)
        {
            var localVarPath = "/contact/{id}/employers";
            localVarPath = localVarPath.Replace("{id}", id.ToString(CultureInfo.InvariantCulture));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetContactEmployerDetails", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetContactEmployerDetails"/>
        public Task<JToken> GetContactEmployerDetailsAsync(long id, string fields = null)
        {
            return GetContactEmployerDetailsAsync(id, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetContactEmployerDetails"/>
        /// <param name="id">The explicitly selected personal contact master identifier.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<JToken> GetContactEmployerDetailsAsync(long id, string fields, CancellationToken cancellationToken)
        {
            return (await GetContactEmployerDetailsAsyncWithHttpInfo(id, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetContactEmployerDetailsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetContactEmployerDetailsAsyncWithHttpInfo(long id, string fields = null)
        {
            return GetContactEmployerDetailsAsyncWithHttpInfo(id, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetContactEmployerDetailsWithHttpInfo"/>
        /// <param name="id">The explicitly selected personal contact master identifier.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetContactEmployerDetailsAsyncWithHttpInfo(long id, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/contact/{id}/employers";
            localVarPath = localVarPath.Replace("{id}", id.ToString(CultureInfo.InvariantCulture));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetContactEmployerDetails", response, ExceptionFactory);
        }

        /// <summary>Retrieves the complete JSON custom properties of a selected contact.</summary>
        /// <param name="id">The explicitly selected contact master identifier.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <returns>The complete JSON response, or null for an empty response body.</returns>
        /// <remarks>The server requires the Kontakte (Anzeigen) profile and documents 403 for missing read rights. No fixed object or collection result shape is declared. The manufacturer declares no fixed success schema. Existing generated methods and their null-data behavior remain unchanged. Fractional values use .NET decimal precision and may round or exceed its range; raw JSON remains exact.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken GetContactPropertiesDetails(long id, string fields = null)
        {
            return GetContactPropertiesDetailsWithHttpInfo(id, fields).Data;
        }

        /// <inheritdoc cref="GetContactPropertiesDetails"/>
        /// <returns>The status, headers, unchanged raw response and complete parsed JSON.</returns>
        public ApiResponse<JToken> GetContactPropertiesDetailsWithHttpInfo(long id, string fields = null)
        {
            var localVarPath = "/contact/{id}/properties";
            localVarPath = localVarPath.Replace("{id}", id.ToString(CultureInfo.InvariantCulture));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetContactPropertiesDetails", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetContactPropertiesDetails"/>
        public Task<JToken> GetContactPropertiesDetailsAsync(long id, string fields = null)
        {
            return GetContactPropertiesDetailsAsync(id, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetContactPropertiesDetails"/>
        /// <param name="id">The explicitly selected contact master identifier.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<JToken> GetContactPropertiesDetailsAsync(long id, string fields, CancellationToken cancellationToken)
        {
            return (await GetContactPropertiesDetailsAsyncWithHttpInfo(id, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetContactPropertiesDetailsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetContactPropertiesDetailsAsyncWithHttpInfo(long id, string fields = null)
        {
            return GetContactPropertiesDetailsAsyncWithHttpInfo(id, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetContactPropertiesDetailsWithHttpInfo"/>
        /// <param name="id">The explicitly selected contact master identifier.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetContactPropertiesDetailsAsyncWithHttpInfo(long id, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/contact/{id}/properties";
            localVarPath = localVarPath.Replace("{id}", id.ToString(CultureInfo.InvariantCulture));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetContactPropertiesDetails", response, ExceptionFactory);
        }

        /// <summary>Retrieves the complete JSON details of a selected contact property.</summary>
        /// <param name="propertyId">The explicitly selected contact property identifier.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <returns>The complete JSON response, or null for an empty response body.</returns>
        /// <remarks>The server requires the Kontakte bearbeiten (Anzeigen) profile. No fixed success schema or complete error-status list is declared. The manufacturer declares no fixed success schema. Existing generated methods and their null-data behavior remain unchanged. Fractional values use .NET decimal precision and may round or exceed its range; raw JSON remains exact.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or a fractional value exceeds the decimal range.</exception>
        public JToken GetContactPropertyDetails(long propertyId, string fields = null)
        {
            return GetContactPropertyDetailsWithHttpInfo(propertyId, fields).Data;
        }

        /// <inheritdoc cref="GetContactPropertyDetails"/>
        /// <returns>The status, headers, unchanged raw response and complete parsed JSON.</returns>
        public ApiResponse<JToken> GetContactPropertyDetailsWithHttpInfo(long propertyId, string fields = null)
        {
            var localVarPath = "/contact/contactProperty/{propertyId}";
            localVarPath = localVarPath.Replace("{propertyId}", propertyId.ToString(CultureInfo.InvariantCulture));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("GetContactPropertyDetails", response, ExceptionFactory);
        }

        /// <inheritdoc cref="GetContactPropertyDetails"/>
        public Task<JToken> GetContactPropertyDetailsAsync(long propertyId, string fields = null)
        {
            return GetContactPropertyDetailsAsync(propertyId, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetContactPropertyDetails"/>
        /// <param name="propertyId">The explicitly selected contact property identifier.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<JToken> GetContactPropertyDetailsAsync(long propertyId, string fields, CancellationToken cancellationToken)
        {
            return (await GetContactPropertyDetailsAsyncWithHttpInfo(propertyId, fields, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="GetContactPropertyDetailsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> GetContactPropertyDetailsAsyncWithHttpInfo(long propertyId, string fields = null)
        {
            return GetContactPropertyDetailsAsyncWithHttpInfo(propertyId, fields, CancellationToken.None);
        }

        /// <inheritdoc cref="GetContactPropertyDetailsWithHttpInfo"/>
        /// <param name="propertyId">The explicitly selected contact property identifier.</param>
        /// <param name="fields">The original optional comma-separated response field selection; null omits it and an empty string is retained.</param>
        /// <param name="cancellationToken">The token used to cancel admission and native active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">Cancellation is requested before or during the request.</exception>
        public async Task<ApiResponse<JToken>> GetContactPropertyDetailsAsyncWithHttpInfo(long propertyId, string fields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/contact/contactProperty/{propertyId}";
            localVarPath = localVarPath.Replace("{propertyId}", propertyId.ToString(CultureInfo.InvariantCulture));
            var queryParams = new List<KeyValuePair<string, string>>();
            if (fields != null) queryParams.Add(new KeyValuePair<string, string>("fields", fields));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Get,
                queryParams, null, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("GetContactPropertyDetails", response, ExceptionFactory);
        }

        private static void ValidateContactReadSelector(string value, string name)
        {
            if (string.IsNullOrWhiteSpace(value) || value == "." || value == "..")
                throw new ArgumentException("An explicit contact key and identifier are required.", name);
        }
    }
}
