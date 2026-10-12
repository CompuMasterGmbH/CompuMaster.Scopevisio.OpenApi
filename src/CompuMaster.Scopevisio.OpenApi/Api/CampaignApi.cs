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
    /// <summary>Provides campaign and campaign-run-entry structured searches.</summary>
    /// <remarks>Uses the shared configuration and existing transport/exception contracts. Profiles, visibility, paging and current server acceptance remain server-enforced.</remarks>
    public partial class CampaignApi
    {
        private ExceptionFactory exceptionFactory = CompuMaster.Scopevisio.OpenApi.Client.Configuration.DefaultExceptionFactory;

        /// <summary>Initializes the API group with the selected shared configuration.</summary>
        /// <param name="configuration">The configuration to use; null selects Configuration.Default without altering it.</param>
        public CampaignApi(Configuration configuration = null)
        {
            Configuration = configuration ?? CompuMaster.Scopevisio.OpenApi.Client.Configuration.Default;
        }

        /// <summary>Initializes the API group with a separate configuration for the selected REST origin.</summary>
        /// <param name="basePath">The REST origin used by the new configuration.</param>
        public CampaignApi(string basePath) : this(new Configuration { BasePath = basePath }) { }

        /// <summary>Gets or sets the configuration used by the API group.</summary>
        /// <value>The configuration shared by requests, including caller-owned HTTP transport and authorization.</value>
        public Configuration Configuration { get; set; }

        /// <summary>Gets or sets the response exception factory.</summary>
        /// <value>The factory used before response parsing; null disables this hook.</value>
        /// <exception cref="InvalidOperationException">The selected factory is a multicast delegate.</exception>
        public ExceptionFactory ExceptionFactory
        {
            get
            {
                if (exceptionFactory != null && exceptionFactory.GetInvocationList().Length > 1)
                    throw new InvalidOperationException("Multicast delegate for ExceptionFactory is unsupported.");
                return exceptionFactory;
            }
            set { exceptionFactory = value; }
        }

        /// <summary>Searches campaigns using a structured JSON object.</summary>
        /// <param name="query">The optional structured search object; null sends an empty object without client defaults.</param>
        /// <returns>The complete JSON result, or null for an empty response.</returns>
        /// <remarks>The vendor's search-body schema says string while its search documentation describes an object; this additive method sends one JSON object. Unset search properties retain server defaults, and caller-selected filters/nulls/paging/future fields are unchanged. Unknown fields, date-like strings and exact raw responses remain available. Fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact. Profiles, visibility and server-specific formats remain server-enforced; no completeness or extra paging is inferred.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response has invalid JSON, an incompatible typed object/record shape or a fraction exceeding the decimal range.</exception>
        public JToken SearchCampaigns(SearchRequest query = null)
        {
            return SearchCampaignsWithHttpInfo(query).Data;
        }

        /// <inheritdoc cref="SearchCampaigns"/>
        /// <returns>The status, headers, exact raw response and complete JSON.</returns>
        public ApiResponse<JToken> SearchCampaignsWithHttpInfo(SearchRequest query = null)
        {
            var localVarPath = "/campaigns";
            var queryParams = new List<KeyValuePair<string, string>>();
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                queryParams, (query ?? new SearchRequest()).ToString(Newtonsoft.Json.Formatting.None), SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("SearchCampaigns", response, ExceptionFactory);
        }

        /// <inheritdoc cref="SearchCampaigns"/>
        public Task<JToken> SearchCampaignsAsync(SearchRequest query = null)
        {
            return SearchCampaignsAsync(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchCampaigns"/>
        /// <param name="query">The optional structured search object; null sends an empty object without client defaults.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> SearchCampaignsAsync(SearchRequest query, CancellationToken cancellationToken)
        {
            return (await SearchCampaignsAsyncWithHttpInfo(query, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="SearchCampaignsWithHttpInfo"/>
        public Task<ApiResponse<JToken>> SearchCampaignsAsyncWithHttpInfo(SearchRequest query = null)
        {
            return SearchCampaignsAsyncWithHttpInfo(query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchCampaignsWithHttpInfo"/>
        /// <param name="query">The optional structured search object; null sends an empty object without client defaults.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> SearchCampaignsAsyncWithHttpInfo(SearchRequest query, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/campaigns";
            var queryParams = new List<KeyValuePair<string, string>>();
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                queryParams, (query ?? new SearchRequest()).ToString(Newtonsoft.Json.Formatting.None), SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("SearchCampaigns", response, ExceptionFactory);
        }

        /// <summary>Searches the selected campaign's run entries.</summary>
        /// <param name="campaignId">The required Int64 campaign identifier, sent unchanged.</param>
        /// <param name="query">The optional structured search object; null sends an empty object without client defaults.</param>
        /// <returns>The complete JSON result, or null for an empty response.</returns>
        /// <remarks>The vendor's search-body schema says string while its search documentation describes an object; this additive method sends one JSON object. Unset search properties retain server defaults, and caller-selected filters/nulls/paging/future fields are unchanged. Unknown fields, date-like strings and exact raw responses remain available. Fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact. Profiles, visibility and server-specific formats remain server-enforced; no completeness or extra paging is inferred.</remarks>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="JsonException">The response has invalid JSON, an incompatible typed object/record shape or a fraction exceeding the decimal range.</exception>
        public JToken SearchCampaignRunEntries(long campaignId, SearchRequest query = null)
        {
            return SearchCampaignRunEntriesWithHttpInfo(campaignId, query).Data;
        }

        /// <inheritdoc cref="SearchCampaignRunEntries"/>
        /// <returns>The status, headers, exact raw response and complete JSON.</returns>
        public ApiResponse<JToken> SearchCampaignRunEntriesWithHttpInfo(long campaignId, SearchRequest query = null)
        {
            var localVarPath = "/campaign/{campaignId}/campaignRunEntries";
            localVarPath = localVarPath.Replace("{campaignId}", Uri.EscapeDataString(campaignId.ToString(CultureInfo.InvariantCulture)));
            var queryParams = new List<KeyValuePair<string, string>>();
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                queryParams, (query ?? new SearchRequest()).ToString(Newtonsoft.Json.Formatting.None), SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("SearchCampaignRunEntries", response, ExceptionFactory);
        }

        /// <inheritdoc cref="SearchCampaignRunEntries"/>
        public Task<JToken> SearchCampaignRunEntriesAsync(long campaignId, SearchRequest query = null)
        {
            return SearchCampaignRunEntriesAsync(campaignId, query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchCampaignRunEntries"/>
        /// <param name="campaignId">The required Int64 campaign identifier, sent unchanged.</param>
        /// <param name="query">The optional structured search object; null sends an empty object without client defaults.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<JToken> SearchCampaignRunEntriesAsync(long campaignId, SearchRequest query, CancellationToken cancellationToken)
        {
            return (await SearchCampaignRunEntriesAsyncWithHttpInfo(campaignId, query, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="SearchCampaignRunEntriesWithHttpInfo"/>
        public Task<ApiResponse<JToken>> SearchCampaignRunEntriesAsyncWithHttpInfo(long campaignId, SearchRequest query = null)
        {
            return SearchCampaignRunEntriesAsyncWithHttpInfo(campaignId, query, CancellationToken.None);
        }

        /// <inheritdoc cref="SearchCampaignRunEntriesWithHttpInfo"/>
        /// <param name="campaignId">The required Int64 campaign identifier, sent unchanged.</param>
        /// <param name="query">The optional structured search object; null sends an empty object without client defaults.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during the request.</exception>
        public async Task<ApiResponse<JToken>> SearchCampaignRunEntriesAsyncWithHttpInfo(long campaignId, SearchRequest query, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var localVarPath = "/campaign/{campaignId}/campaignRunEntries";
            localVarPath = localVarPath.Replace("{campaignId}", Uri.EscapeDataString(campaignId.ToString(CultureInfo.InvariantCulture)));
            var queryParams = new List<KeyValuePair<string, string>>();
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                queryParams, (query ?? new SearchRequest()).ToString(Newtonsoft.Json.Formatting.None), SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("SearchCampaignRunEntries", response, ExceptionFactory);
        }

    }
}
