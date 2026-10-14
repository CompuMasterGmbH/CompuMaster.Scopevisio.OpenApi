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
        /// <summary>Creates a sales project using explicitly supplied current fields.</summary>
        /// <param name="request">The optional complete project JSON, copied before HTTP I/O without inserting defaults. Null sends no body.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 200 without a fixed successful schema; complete future JSON is retained.</returns>
        /// <remarks>The server enforces Projekte (Bearbeiten), project/contact/user/employee/catalogue/product/dimension/budget/permission semantics. Both route bodies are optional; a supplied creation object requires name and contactId, while update can retain sparse fields and explicit nulls under general update rules. Server 400 still reports actual required/contact/member/uniqueness failures. Creation sets sphereLead; REST cannot convert a lead to sphereInterested. Interested fields only apply to already-interested projects; no retrieval, conversion or state inference is performed. User-contact salesperson/marketing IDs and decision-maker employment references remain distinct original values without UID/contact/team-entry translation or lookup. Original catalogue strings, ¤¤¤ multi-product separators, Int64 epochs, Int32 workdays/permissions, finite numbers, nullable flags and deeply copied child/future JSON are preserved without defaults, splitting, calculations, array merges, account-context changes or extra writes. The manufacturer declares HTTP 200 without a fixed successful schema for both operations, unlike normal-project creation 201; no project ID or aggregate success is invented. 404 can mean missing project or authorization. Cancellation after sending cannot establish resulting state; no mutation replay occurs. Parsed fractions use bounded decimal precision; raw JSON remains exact. Actual sales/lead/interested/profile behavior and owned reversible cleanup remain unverified; legacy ProjectApi ownership and APIs/defaults remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A known field has an incompatible JSON kind, an integer exceeds its declared width, a number is not finite, or a supplied creation object lacks required fields.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public Newtonsoft.Json.Linq.JToken CreateSalesProjectWithResult(SalesProjectWriteRequest request = null)
        {
            return CreateSalesProjectWithResultWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="CreateSalesProjectWithResult"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete operation result.</returns>
        public ApiResponse<Newtonsoft.Json.Linq.JToken> CreateSalesProjectWithResultWithHttpInfo(SalesProjectWriteRequest request = null)
        {
            var body = request?.SnapshotForSend(true);
            var localVarPath = "/salesproject/new";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("CreateSalesProjectWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="CreateSalesProjectWithResult"/>
        public Task<Newtonsoft.Json.Linq.JToken> CreateSalesProjectWithResultAsync(SalesProjectWriteRequest request = null)
        {
            return CreateSalesProjectWithResultAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreateSalesProjectWithResult"/>
        /// <param name="request">The optional complete project JSON, copied before HTTP I/O. Null sends no body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have created the project.</exception>
        public async Task<Newtonsoft.Json.Linq.JToken> CreateSalesProjectWithResultAsync(SalesProjectWriteRequest request, CancellationToken cancellationToken)
        {
            return (await CreateSalesProjectWithResultAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="CreateSalesProjectWithResultWithHttpInfo"/>
        public Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> CreateSalesProjectWithResultAsyncWithHttpInfo(SalesProjectWriteRequest request = null)
        {
            return CreateSalesProjectWithResultAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreateSalesProjectWithResultWithHttpInfo"/>
        /// <param name="request">The optional complete project JSON, copied before HTTP I/O. Null sends no body.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have created the project.</exception>
        public async Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> CreateSalesProjectWithResultAsyncWithHttpInfo(SalesProjectWriteRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = request?.SnapshotForSend(true);
            var localVarPath = "/salesproject/new";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("CreateSalesProjectWithResult", response, ExceptionFactory);
        }
    }
}
