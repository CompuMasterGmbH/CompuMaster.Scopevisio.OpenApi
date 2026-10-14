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
    public partial class UserApi
    {
        /// <summary>Updates explicitly supplied user fields and assignments.</summary>
        /// <param name="request">The optional complete copied user array; null sends no body and empty arrays remain empty without identity, privilege, profile, license or role defaults.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 200 without a fixed success schema.</returns>
        /// <remarks>The server enforces Benutzer (Bearbeiten), user/login/profile/license/role semantics. Body is an optional array; creation rows require original email, firstName and lastName, while updates preserve sparse fields and optional raw nulls under general update rules. No UID/email XOR, precedence, identity lookup, email normalization, default user, profile merge, license purchase/expiry or privilege widening is introduced. Read-only status, specialRights, creationTs and license validTill cannot be written. Profile organisation names or Instanz scopes are distinct from numeric Scope organisation and opaque Teamwork tenant identifiers; assignment scope can exceed the selected organisation. Firmen und Benutzer requires administrator true on the server, but the client never inserts it. Explicit administrator/password-change/login-renaming/profile/license/Teamwork-role choices remain server-evaluated without separate requests or context selection. Complete batches and nested assignments/future JSON are copied before HTTP without retry or mutation replay. HTTP 200 has no fixed successful schema or guaranteed per-user/aggregate success/atomicity. Complete response/read-only/future data remain available; parsed fractions use bounded decimal precision and raw JSON remains exact. Cancellation after sending cannot establish user/login/assignment state. Actual user/profile/assignment behavior and separately owned reversible restoration remain unverified; historical APIs/defaults/legacy-only fields remain unchanged.</remarks>
        /// <exception cref="ArgumentException">A supplied row or assignment is not an object, a known field has an incompatible kind, a creation row or supplied assignment lacks required fields, or a known read-only field is supplied.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public Newtonsoft.Json.Linq.JToken UpdateUsersWithResult(UserBatchWriteRequest request = null)
        {
            return UpdateUsersWithResultWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="UpdateUsersWithResult"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete operation result.</returns>
        public ApiResponse<Newtonsoft.Json.Linq.JToken> UpdateUsersWithResultWithHttpInfo(UserBatchWriteRequest request = null)
        {
            var body = request?.SnapshotForSend(false);
            var localVarPath = "/users";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Put,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("UpdateUsersWithResult", response, ExceptionFactory);
        }

        /// <inheritdoc cref="UpdateUsersWithResult"/>
        public Task<Newtonsoft.Json.Linq.JToken> UpdateUsersWithResultAsync(UserBatchWriteRequest request = null)
        {
            return UpdateUsersWithResultAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="UpdateUsersWithResult"/>
        /// <param name="request">The optional complete copied user array; null sends no body and empty arrays remain empty without identity, privilege, profile, license or role defaults.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have changed users or assignments.</exception>
        public async Task<Newtonsoft.Json.Linq.JToken> UpdateUsersWithResultAsync(UserBatchWriteRequest request, CancellationToken cancellationToken)
        {
            return (await UpdateUsersWithResultAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="UpdateUsersWithResultWithHttpInfo"/>
        public Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> UpdateUsersWithResultAsyncWithHttpInfo(UserBatchWriteRequest request = null)
        {
            return UpdateUsersWithResultAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="UpdateUsersWithResultWithHttpInfo"/>
        /// <param name="request">The optional complete copied user array; null sends no body and empty arrays remain empty without identity, privilege, profile, license or role defaults.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have changed users or assignments.</exception>
        public async Task<ApiResponse<Newtonsoft.Json.Linq.JToken>> UpdateUsersWithResultAsyncWithHttpInfo(UserBatchWriteRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = request?.SnapshotForSend(false);
            var localVarPath = "/users";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Put,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("UpdateUsersWithResult", response, ExceptionFactory);
        }
    }
}
