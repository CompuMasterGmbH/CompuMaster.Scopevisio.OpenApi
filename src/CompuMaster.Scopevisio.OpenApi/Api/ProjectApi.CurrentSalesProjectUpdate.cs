using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.Api
{
    public partial class ProjectApi
    {
        /// <summary>Updates explicitly supplied fields of a sales project.</summary>
        /// <param name="id">The explicit original signed Int64 project identifier, sent using invariant text without lookup or default IDs.</param>
        /// <param name="request">The optional complete current project JSON, copied before HTTP I/O. Null omits the body; omission does not insert defaults or clear existing values.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 200 without a fixed success schema.</returns>
        /// <remarks>The server enforces Projekte (Bearbeiten), project/contact/user/employee/catalogue/product/dimension/budget/permission semantics. Both route bodies are optional; a supplied creation object requires name and contactId, while update can retain sparse fields and explicit nulls under general update rules. Server 400 still reports actual required/contact/member/uniqueness failures. Creation sets sphereLead; REST cannot convert a lead to sphereInterested. Interested fields only apply to already-interested projects; no retrieval, conversion or state inference is performed. User-contact salesperson/marketing IDs and decision-maker employment references remain distinct original values without UID/contact/team-entry translation or lookup. Original catalogue strings, ¤¤¤ multi-product separators, Int64 epochs, Int32 workdays/permissions, finite numbers, nullable flags and deeply copied child/future JSON are preserved without defaults, splitting, calculations, array merges, account-context changes or extra writes. The manufacturer declares HTTP 200 without a fixed successful schema for both operations, unlike normal-project creation 201; no project ID or aggregate success is invented. 404 can mean missing project or authorization. Cancellation after sending cannot establish resulting state; no mutation replay occurs. Parsed fractions use bounded decimal precision; raw JSON remains exact. Actual sales/lead/interested/profile behavior and owned reversible cleanup remain unverified; legacy ProjectApi ownership and APIs/defaults remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A known field has an incompatible JSON kind, an integer exceeds its declared width, a number is not finite, or a supplied creation object lacks required fields.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public Newtonsoft.Json.Linq.JToken UpdateSalesProjectWithResult(long id, SalesProjectWriteRequest request = null)
        {
            return UpdateSalesProjectWithResultWithHttpInfo(id, request).Data;
        }

        /// <inheritdoc cref="UpdateSalesProjectWithResult"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete operation result.</returns>
        public ApiResponse<Newtonsoft.Json.Linq.JToken> UpdateSalesProjectWithResultWithHttpInfo(long id, SalesProjectWriteRequest request = null)
        {
            var body = request?.SnapshotForSend(false);
            var localVarPath = "/salesproject/{id}";
            localVarPath = localVarPath.Replace("{id}", Uri.EscapeDataString(id.ToString(CultureInfo.InvariantCulture)));
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("UpdateSalesProjectWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="UpdateSalesProjectWithResult"/>
        public Task<Newtonsoft.Json.Linq.JToken> UpdateSalesProjectWithResultAsync(long id, SalesProjectWriteRequest request = null)
        {
            return UpdateSalesProjectWithResultAsync(id, request, CancellationToken.None);
        }

        /// <inheritdoc cref="UpdateSalesProjectWithResult"/>
        /// <param name="id">The explicit original signed Int64 project identifier, sent using invariant text without lookup or default IDs.</param>
        /// <param name="request">The optional complete project JSON, copied before HTTP I/O; null omits the body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied the update.</exception>
        public async Task<Newtonsoft.Json.Linq.JToken> UpdateSalesProjectWithResultAsync(long id, SalesProjectWriteRequest request, CancellationToken cancellationToken)
        {
            return (await UpdateSalesProjectWithResultAsyncWithHttpInfo(id, request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="UpdateSalesProjectWithResultWithHttpInfo"/>
        public Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> UpdateSalesProjectWithResultAsyncWithHttpInfo(long id, SalesProjectWriteRequest request = null)
        {
            return UpdateSalesProjectWithResultAsyncWithHttpInfo(id, request, CancellationToken.None);
        }

        /// <inheritdoc cref="UpdateSalesProjectWithResultWithHttpInfo"/>
        /// <param name="id">The explicit original signed Int64 project identifier, sent using invariant text without lookup or default IDs.</param>
        /// <param name="request">The optional complete project JSON, copied before HTTP I/O; null omits the body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied the update.</exception>
        public async Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> UpdateSalesProjectWithResultAsyncWithHttpInfo(long id, SalesProjectWriteRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = request?.SnapshotForSend(false);
            var localVarPath = "/salesproject/{id}";
            localVarPath = localVarPath.Replace("{id}", Uri.EscapeDataString(id.ToString(CultureInfo.InvariantCulture)));
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("UpdateSalesProjectWithResult", response, ExceptionFactory);
        }
    }
}
