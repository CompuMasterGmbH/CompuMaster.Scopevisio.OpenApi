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
    /// <summary>Provides explicit inventory credit and purchase delivery/order workflows.</summary>
    /// <remarks>Uses the shared configuration and existing transport/exception contracts. The manufacturer groups these endpoints as BETA. Profiles, product/vendor/unit resolution, business dates and genuine versus candidate outcomes remain server-enforced.</remarks>
    public partial class MaterialManagementApi
    {
        private ExceptionFactory exceptionFactory = CompuMaster.Scopevisio.OpenApi.Client.Configuration.DefaultExceptionFactory;

        /// <summary>Initializes the API group with the selected shared configuration.</summary>
        /// <param name="configuration">The configuration to use; null selects Configuration.Default without altering it.</param>
        public MaterialManagementApi(Configuration configuration = null)
        {
            Configuration = configuration ?? CompuMaster.Scopevisio.OpenApi.Client.Configuration.Default;
        }

        /// <summary>Initializes the API group with a separate configuration for the selected REST origin.</summary>
        /// <param name="basePath">The REST origin used by the new configuration.</param>
        public MaterialManagementApi(string basePath) : this(new Configuration { BasePath = basePath }) { }

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

        /// <summary>Decreases product inventory using explicitly selected material credit rows.</summary>
        /// <param name="request">The optional complete JSON body; null omits it. Supplied objects are deeply copied and declared required fields and known JSON kinds/widths are validated.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 200 with MaterialResponse; the complete original JSON and feedback are retained without guessed candidate or aggregate success.</returns>
        /// <remarks>The server enforces Bestandsverwaltung (Bearbeiten), product ID precedence, available inventory/warehouse and project dimensions. The schema requires both product ID and number while prose treats them as alternatives; this client accepts either explicit identity and retains both when supplied. No products, warehouses, prices or dimension values are inferred. No account/item/date/period default, current state, count or transaction result is inferred. No new mutation retry or partial-failure replay is added; cancellation after sending cannot establish which changes occurred. Parsed response fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentException">A supplied required value is absent or a known nested field has an incompatible JSON kind or width.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public JToken CreditMaterialInventory(MaterialInventoryCreditRequest request = null)
        {
            return CreditMaterialInventoryWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="CreditMaterialInventory"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete result.</returns>
        public ApiResponse<JToken> CreditMaterialInventoryWithHttpInfo(MaterialInventoryCreditRequest request = null)
        {
            var body = SnapshotMaterialRequest(request, 0);
            var localVarPath = "/material/posting/credit";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("CreditMaterialInventory", response, ExceptionFactory);
        }

        /// <inheritdoc cref="CreditMaterialInventory"/>
        public Task<JToken> CreditMaterialInventoryAsync(MaterialInventoryCreditRequest request = null)
        {
            return CreditMaterialInventoryAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreditMaterialInventory"/>
        /// <param name="request">The optional complete JSON body; null omits it. Supplied objects are deeply copied and declared required fields and known JSON kinds/widths are validated.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied some or all changes.</exception>
        public async Task<JToken> CreditMaterialInventoryAsync(MaterialInventoryCreditRequest request, CancellationToken cancellationToken)
        {
            return (await CreditMaterialInventoryAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="CreditMaterialInventoryWithHttpInfo"/>
        public Task<ApiResponse<JToken>> CreditMaterialInventoryAsyncWithHttpInfo(MaterialInventoryCreditRequest request = null)
        {
            return CreditMaterialInventoryAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreditMaterialInventoryWithHttpInfo"/>
        /// <param name="request">The optional complete JSON body; null omits it. Supplied objects are deeply copied and declared required fields and known JSON kinds/widths are validated.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied some or all changes.</exception>
        public async Task<ApiResponse<JToken>> CreditMaterialInventoryAsyncWithHttpInfo(MaterialInventoryCreditRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = SnapshotMaterialRequest(request, 0);
            var localVarPath = "/material/posting/credit";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("CreditMaterialInventory", response, ExceptionFactory);
        }

        /// <summary>Creates an explicitly supplied purchase delivery or candidate.</summary>
        /// <param name="request">The required complete JSON body. Supplied objects are deeply copied and declared required fields and known JSON kinds/widths are validated.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 200 with MaterialResponse; the complete original JSON and feedback are retained without guessed candidate or aggregate success.</returns>
        /// <remarks>The server enforces Wareneingang (Bearbeiten), actual vendor/product/unit resolution and dates. HTTP 200 can create a genuine document or a candidate; complete MaterialResponse results and feedback must be inspected. No catalog or posting defaults are inferred. No account/item/date/period default, current state, count or transaction result is inferred. No new mutation retry or partial-failure replay is added; cancellation after sending cannot establish which changes occurred. Parsed response fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentNullException">The required request is null.</exception>
        /// <exception cref="ArgumentException">A supplied required value is absent or a known nested field has an incompatible JSON kind or width.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public JToken CreateMaterialPurchaseDelivery(MaterialPurchaseDeliveryCreationRequest request)
        {
            return CreateMaterialPurchaseDeliveryWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="CreateMaterialPurchaseDelivery"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete result.</returns>
        public ApiResponse<JToken> CreateMaterialPurchaseDeliveryWithHttpInfo(MaterialPurchaseDeliveryCreationRequest request)
        {
            var body = SnapshotMaterialRequest(request, 1);
            var localVarPath = "/material/purchasedelivery/new";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("CreateMaterialPurchaseDelivery", response, ExceptionFactory);
        }

        /// <inheritdoc cref="CreateMaterialPurchaseDelivery"/>
        public Task<JToken> CreateMaterialPurchaseDeliveryAsync(MaterialPurchaseDeliveryCreationRequest request)
        {
            return CreateMaterialPurchaseDeliveryAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreateMaterialPurchaseDelivery"/>
        /// <param name="request">The required complete JSON body. Supplied objects are deeply copied and declared required fields and known JSON kinds/widths are validated.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied some or all changes.</exception>
        public async Task<JToken> CreateMaterialPurchaseDeliveryAsync(MaterialPurchaseDeliveryCreationRequest request, CancellationToken cancellationToken)
        {
            return (await CreateMaterialPurchaseDeliveryAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="CreateMaterialPurchaseDeliveryWithHttpInfo"/>
        public Task<ApiResponse<JToken>> CreateMaterialPurchaseDeliveryAsyncWithHttpInfo(MaterialPurchaseDeliveryCreationRequest request)
        {
            return CreateMaterialPurchaseDeliveryAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreateMaterialPurchaseDeliveryWithHttpInfo"/>
        /// <param name="request">The required complete JSON body. Supplied objects are deeply copied and declared required fields and known JSON kinds/widths are validated.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied some or all changes.</exception>
        public async Task<ApiResponse<JToken>> CreateMaterialPurchaseDeliveryAsyncWithHttpInfo(MaterialPurchaseDeliveryCreationRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = SnapshotMaterialRequest(request, 1);
            var localVarPath = "/material/purchasedelivery/new";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("CreateMaterialPurchaseDelivery", response, ExceptionFactory);
        }

        /// <summary>Posts an explicitly selected purchase delivery at an original requested date.</summary>
        /// <param name="idOrDocumentNumber">The required original purchase-delivery master ID or document number, escaped without numeric parsing or trimming.</param>
        /// <param name="request">The optional complete JSON request; null omits the body. A supplied object requires the original postingDate string.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 200 with MaterialResponse; the complete original JSON and feedback are retained without guessed candidate or aggregate success.</returns>
        /// <remarks>The server enforces Wareneingang (Bearbeiten) and adjusts the original requested posting date to business rules. Conflicting yyyy-MM-dd/dd.MM.yyyy documentation is retained without client date conversion. Complete MaterialResponse results/feedback are preserved; HTTP 200 does not infer transactionality or absence of item failures. No new mutation retry or follow-up is added; cancellation after sending cannot establish the server outcome. Parsed fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentException">The purchase-delivery selection is blank or a relative dot segment, or known supplied fields have incompatible required kinds or widths.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public JToken PostMaterialPurchaseDelivery(string idOrDocumentNumber, MaterialPurchaseDeliveryPostingRequest request = null)
        {
            return PostMaterialPurchaseDeliveryWithHttpInfo(idOrDocumentNumber, request).Data;
        }

        /// <inheritdoc cref="PostMaterialPurchaseDelivery"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete result.</returns>
        public ApiResponse<JToken> PostMaterialPurchaseDeliveryWithHttpInfo(string idOrDocumentNumber, MaterialPurchaseDeliveryPostingRequest request = null)
        {
            var selection = ValidateMaterialSelection(idOrDocumentNumber);
            var body = SnapshotMaterialRequest(request, 2);
            var localVarPath = "/material/purchasedelivery/{idOrDocumentNumber}/post";
            localVarPath = localVarPath.Replace("{idOrDocumentNumber}", selection);
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("PostMaterialPurchaseDelivery", response, ExceptionFactory);
        }

        /// <inheritdoc cref="PostMaterialPurchaseDelivery"/>
        public Task<JToken> PostMaterialPurchaseDeliveryAsync(string idOrDocumentNumber, MaterialPurchaseDeliveryPostingRequest request = null)
        {
            return PostMaterialPurchaseDeliveryAsync(idOrDocumentNumber, request, CancellationToken.None);
        }

        /// <inheritdoc cref="PostMaterialPurchaseDelivery"/>
        /// <param name="idOrDocumentNumber">The required original purchase-delivery master ID or document number, escaped without numeric parsing or trimming.</param>
        /// <param name="request">The optional complete JSON request; null omits the body. A supplied object requires the original postingDate string.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have posted the delivery.</exception>
        public async Task<JToken> PostMaterialPurchaseDeliveryAsync(string idOrDocumentNumber, MaterialPurchaseDeliveryPostingRequest request, CancellationToken cancellationToken)
        {
            return (await PostMaterialPurchaseDeliveryAsyncWithHttpInfo(idOrDocumentNumber, request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="PostMaterialPurchaseDeliveryWithHttpInfo"/>
        public Task<ApiResponse<JToken>> PostMaterialPurchaseDeliveryAsyncWithHttpInfo(string idOrDocumentNumber, MaterialPurchaseDeliveryPostingRequest request = null)
        {
            return PostMaterialPurchaseDeliveryAsyncWithHttpInfo(idOrDocumentNumber, request, CancellationToken.None);
        }

        /// <inheritdoc cref="PostMaterialPurchaseDeliveryWithHttpInfo"/>
        /// <param name="idOrDocumentNumber">The required original purchase-delivery master ID or document number, escaped without numeric parsing or trimming.</param>
        /// <param name="request">The optional complete JSON request; null omits the body. A supplied object requires the original postingDate string.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have posted the delivery.</exception>
        public async Task<ApiResponse<JToken>> PostMaterialPurchaseDeliveryAsyncWithHttpInfo(string idOrDocumentNumber, MaterialPurchaseDeliveryPostingRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var selection = ValidateMaterialSelection(idOrDocumentNumber);
            var body = SnapshotMaterialRequest(request, 2);
            var localVarPath = "/material/purchasedelivery/{idOrDocumentNumber}/post";
            localVarPath = localVarPath.Replace("{idOrDocumentNumber}", selection);
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("PostMaterialPurchaseDelivery", response, ExceptionFactory);
        }

        /// <summary>Creates an explicitly supplied purchase order or candidate.</summary>
        /// <param name="request">The required complete JSON body. Supplied objects are deeply copied and declared required fields and known JSON kinds/widths are validated.</param>
        /// <returns>The complete JSON result, or null for an empty body. The manufacturer declares HTTP 200 with MaterialResponse; the complete original JSON and feedback are retained without guessed candidate or aggregate success.</returns>
        /// <remarks>The server enforces Bestellung (Bearbeiten), actual vendor/product/unit resolution and dates. HTTP 200 can create a genuine document or a candidate; complete MaterialResponse results and feedback must be inspected. No catalog or posting defaults are inferred. No account/item/date/period default, current state, count or transaction result is inferred. No new mutation retry or partial-failure replay is added; cancellation after sending cannot establish which changes occurred. Parsed response fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact.</remarks>
        /// <exception cref="ArgumentNullException">The required request is null.</exception>
        /// <exception cref="ArgumentException">A supplied required value is absent or a known nested field has an incompatible JSON kind or width.</exception>
        /// <exception cref="ApiException">The configured exception factory rejects the response.</exception>
        /// <exception cref="Newtonsoft.Json.JsonException">The response is invalid JSON or exceeds the numeric parser range.</exception>
        public JToken CreateMaterialPurchaseOrder(MaterialPurchaseOrderCreationRequest request)
        {
            return CreateMaterialPurchaseOrderWithHttpInfo(request).Data;
        }

        /// <inheritdoc cref="CreateMaterialPurchaseOrder"/>
        /// <returns>The status, response/content headers, exact raw JSON and complete result.</returns>
        public ApiResponse<JToken> CreateMaterialPurchaseOrderWithHttpInfo(MaterialPurchaseOrderCreationRequest request)
        {
            var body = SnapshotMaterialRequest(request, 3);
            var localVarPath = "/material/purchaseorder/new";
            var response = Configuration.ApiClient.CallApi(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json");
            return SearchJsonResponse.Parse("CreateMaterialPurchaseOrder", response, ExceptionFactory);
        }

        /// <inheritdoc cref="CreateMaterialPurchaseOrder"/>
        public Task<JToken> CreateMaterialPurchaseOrderAsync(MaterialPurchaseOrderCreationRequest request)
        {
            return CreateMaterialPurchaseOrderAsync(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreateMaterialPurchaseOrder"/>
        /// <param name="request">The required complete JSON body. Supplied objects are deeply copied and declared required fields and known JSON kinds/widths are validated.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied some or all changes.</exception>
        public async Task<JToken> CreateMaterialPurchaseOrderAsync(MaterialPurchaseOrderCreationRequest request, CancellationToken cancellationToken)
        {
            return (await CreateMaterialPurchaseOrderAsyncWithHttpInfo(request, cancellationToken).ConfigureAwait(false)).Data;
        }

        /// <inheritdoc cref="CreateMaterialPurchaseOrderWithHttpInfo"/>
        public Task<ApiResponse<JToken>> CreateMaterialPurchaseOrderAsyncWithHttpInfo(MaterialPurchaseOrderCreationRequest request)
        {
            return CreateMaterialPurchaseOrderAsyncWithHttpInfo(request, CancellationToken.None);
        }

        /// <inheritdoc cref="CreateMaterialPurchaseOrderWithHttpInfo"/>
        /// <param name="request">The required complete JSON body. Supplied objects are deeply copied and declared required fields and known JSON kinds/widths are validated.</param>
        /// <param name="cancellationToken">The token used to cancel active HTTP I/O.</param>
        /// <exception cref="OperationCanceledException">The token is canceled before or during transport; the server may already have applied some or all changes.</exception>
        public async Task<ApiResponse<JToken>> CreateMaterialPurchaseOrderAsyncWithHttpInfo(MaterialPurchaseOrderCreationRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var body = SnapshotMaterialRequest(request, 3);
            var localVarPath = "/material/purchaseorder/new";
            var response = await Configuration.ApiClient.CallApiAsync(localVarPath, Method.Post,
                new List<KeyValuePair<string, string>>(), body, SearchJsonResponse.Headers(Configuration),
                new Dictionary<string, string>(), new Dictionary<string, FileParameter>(),
                new Dictionary<string, string>(), "application/json", cancellationToken).ConfigureAwait(false);
            return SearchJsonResponse.Parse("CreateMaterialPurchaseOrder", response, ExceptionFactory);
        }

        private static string ValidateMaterialSelection(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value == "." || value == "..") throw new ArgumentException("An original purchase-delivery selection is required.", "idOrDocumentNumber");
            return Uri.EscapeDataString(value);
        }

        private static string SnapshotMaterialRequest(JObject request, int route)
        {
            if (request == null)
            {
                if (route == 1 || route == 3) throw new ArgumentNullException(nameof(request));
                return null;
            }
            var snapshot = (JObject)request.DeepClone();
            ValidateMaterialObject(snapshot, route == 0 ? 0 : route == 1 ? 2 : route == 2 ? 3 : 4);
            return snapshot.ToString(Newtonsoft.Json.Formatting.None);
        }

        private static void ValidateMaterialObject(JObject value, int kind)
        {
            if (kind == 0)
            {
                ValidateMaterialArray(value["materialPostingRows"], "materialPostingRows", 1);
            }
            else if (kind == 1)
            {
                if (value["quantity"] == null || value["quantity"].Type == JTokenType.Null) throw new ArgumentException("The quantity field is required.", "request");
                if ((value["productId"] == null || value["productId"].Type == JTokenType.Null) && (value["productNumber"] == null || value["productNumber"].Type == JTokenType.Null)) throw new ArgumentException("An explicit product ID or number is required; the manufacturer prose treats these as alternatives.", "request");
                ValidateMaterialValue(value["productId"], "productId", JTokenType.Integer, false);
                ValidateMaterialValue(value["productNumber"], "productNumber", JTokenType.String, false);
                ValidateMaterialValue(value["quantity"], "quantity", JTokenType.Float, true);
                ValidateMaterialValue(value["warehouseId"], "warehouseId", JTokenType.Integer, false);
                ValidateMaterialValue(value["contactId"], "contactId", JTokenType.Integer, false);
                ValidateMaterialValue(value["amount"], "amount", JTokenType.Float, false);
                ValidateMaterialValue(value["price"], "price", JTokenType.Float, false);
                ValidateMaterialValue(value["projectId"], "projectId", JTokenType.Integer, false);
                ValidateMaterialValue(value["dimension1"], "dimension1", JTokenType.Integer, false);
                ValidateMaterialValue(value["dimension2"], "dimension2", JTokenType.Integer, false);
                ValidateMaterialValue(value["dimension3"], "dimension3", JTokenType.Integer, false);
                ValidateMaterialValue(value["postingTs"], "postingTs", JTokenType.Integer, false);
            }
            else if (kind == 2)
            {
                if (value["materialPurchaseDeliveryPositions"] == null || value["materialPurchaseDeliveryPositions"].Type == JTokenType.Null) throw new ArgumentException("The materialPurchaseDeliveryPositions field is required.", "request");
                if (value["purchaseDeliveryDate"] == null || value["purchaseDeliveryDate"].Type == JTokenType.Null) throw new ArgumentException("The purchaseDeliveryDate field is required.", "request");
                if (value["vendor"] == null || value["vendor"].Type == JTokenType.Null) throw new ArgumentException("The vendor field is required.", "request");
                ValidateMaterialChild(value["vendor"], "vendor", 5);
                ValidateMaterialValue(value["purchaseDeliveryDate"], "purchaseDeliveryDate", JTokenType.String, false);
                ValidateMaterialValue(value["externalPurchaseDeliveryNumber"], "externalPurchaseDeliveryNumber", JTokenType.String, false);
                ValidateMaterialValue(value["enableAutomapping"], "enableAutomapping", JTokenType.Boolean, false);
                ValidateMaterialArray(value["referencedPurchaseOrderNumbers"], "referencedPurchaseOrderNumbers", -1);
                ValidateMaterialValue(value["generatePurchaseOrderForRemainingValue"], "generatePurchaseOrderForRemainingValue", JTokenType.Boolean, false);
                ValidateMaterialValue(value["postingDate"], "postingDate", JTokenType.String, false);
                ValidateMaterialValue(value["postDelivery"], "postDelivery", JTokenType.Boolean, false);
                ValidateMaterialArray(value["materialPurchaseDeliveryPositions"], "materialPurchaseDeliveryPositions", 8);
            }
            else if (kind == 3)
            {
                if (value["postingDate"] == null || value["postingDate"].Type == JTokenType.Null) throw new ArgumentException("The postingDate field is required.", "request");
                ValidateMaterialValue(value["postingDate"], "postingDate", JTokenType.String, false);
            }
            else if (kind == 4)
            {
                if (value["materialPurchaseOrderPositions"] == null || value["materialPurchaseOrderPositions"].Type == JTokenType.Null) throw new ArgumentException("The materialPurchaseOrderPositions field is required.", "request");
                if (value["purchaseOrderDate"] == null || value["purchaseOrderDate"].Type == JTokenType.Null) throw new ArgumentException("The purchaseOrderDate field is required.", "request");
                if (value["vendor"] == null || value["vendor"].Type == JTokenType.Null) throw new ArgumentException("The vendor field is required.", "request");
                ValidateMaterialChild(value["vendor"], "vendor", 5);
                ValidateMaterialValue(value["purchaseOrderDate"], "purchaseOrderDate", JTokenType.String, false);
                ValidateMaterialValue(value["externalPurchaseOrderNumber"], "externalPurchaseOrderNumber", JTokenType.String, false);
                ValidateMaterialArray(value["materialPurchaseOrderPositions"], "materialPurchaseOrderPositions", 8);
            }
            else if (kind == 5)
            {
                ValidateMaterialValue(value["vendorId"], "vendorId", JTokenType.Integer, false);
                ValidateMaterialValue(value["externalSystemVendorId"], "externalSystemVendorId", JTokenType.String, false);
            }
            else if (kind == 6)
            {
                ValidateMaterialValue(value["productId"], "productId", JTokenType.Integer, false);
                ValidateMaterialValue(value["externalSystemProductId"], "externalSystemProductId", JTokenType.String, false);
                ValidateMaterialValue(value["productName"], "productName", JTokenType.String, false);
                ValidateMaterialValue(value["productNumber"], "productNumber", JTokenType.String, false);
                ValidateMaterialValue(value["vendorProductNumber"], "vendorProductNumber", JTokenType.String, false);
                ValidateMaterialValue(value["thirdPartyProductNumber"], "thirdPartyProductNumber", JTokenType.String, false);
                ValidateMaterialValue(value["productGtin"], "productGtin", JTokenType.String, false);
                ValidateMaterialValue(value["vendorProductGtin"], "vendorProductGtin", JTokenType.String, false);
            }
            else if (kind == 7)
            {
                ValidateMaterialValue(value["measurementUnitId"], "measurementUnitId", JTokenType.Integer, false);
                ValidateMaterialValue(value["deliveryUnitId"], "deliveryUnitId", JTokenType.Integer, false);
                ValidateMaterialValue(value["externalSystemUnitId"], "externalSystemUnitId", JTokenType.String, false);
                ValidateMaterialValue(value["unitName"], "unitName", JTokenType.String, false);
                ValidateMaterialValue(value["thirdPartyUnitId"], "thirdPartyUnitId", JTokenType.String, false);
                ValidateMaterialValue(value["thirdPartyUnitName"], "thirdPartyUnitName", JTokenType.String, false);
            }
            else if (kind == 8)
            {
                if (value["price"] == null || value["price"].Type == JTokenType.Null) throw new ArgumentException("The price field is required.", "request");
                if (value["product"] == null || value["product"].Type == JTokenType.Null) throw new ArgumentException("The product field is required.", "request");
                if (value["quantity"] == null || value["quantity"].Type == JTokenType.Null) throw new ArgumentException("The quantity field is required.", "request");
                ValidateMaterialChild(value["product"], "product", 6);
                ValidateMaterialValue(value["quantity"], "quantity", JTokenType.Float, true);
                ValidateMaterialChild(value["unit"], "unit", 7);
                ValidateMaterialValue(value["price"], "price", JTokenType.Float, true);
            }
        }

        private static void ValidateMaterialChild(JToken token, string name, int kind)
        {
            if (token == null || token.Type == JTokenType.Null) return;
            if (!(token is JObject value)) throw new ArgumentException("The " + name + " field must be an object or null.", "request");
            ValidateMaterialObject(value, kind);
        }

        private static void ValidateMaterialArray(JToken token, string name, int kind)
        {
            if (token == null || token.Type == JTokenType.Null) return;
            if (!(token is JArray array)) throw new ArgumentException("The " + name + " field must be an array or null.", "request");
            foreach (var entry in array)
            {
                if (kind < 0)
                {
                    if (entry.Type != JTokenType.String) throw new ArgumentException("The " + name + " entries must be original strings.", "request");
                }
                else
                {
                    if (!(entry is JObject value)) throw new ArgumentException("The " + name + " entries must be objects.", "request");
                    ValidateMaterialObject(value, kind);
                }
            }
        }

        private static void ValidateMaterialValue(JToken value, string name, JTokenType kind, bool nonnegative)
        {
            if (value == null || value.Type == JTokenType.Null) return;
            if (kind == JTokenType.Float)
            {
                var text = value.ToString(Formatting.None);
                if ((value.Type != JTokenType.Float && value.Type != JTokenType.Integer) ||
                    !System.Text.RegularExpressions.Regex.IsMatch(text, @"^-?(?:0|[1-9]\d*)(?:\.\d+)?(?:[eE][+-]?\d+)?$"))
                    throw new ArgumentException("The " + name + " field must be a finite JSON number.", "request");
                var mantissa = text.Split('e', 'E')[0];
                if (nonnegative && text.StartsWith("-", StringComparison.Ordinal) && System.Text.RegularExpressions.Regex.IsMatch(mantissa, "[1-9]"))
                    throw new ArgumentException("The " + name + " field has a schema minimum of zero.", "request");
            }
            else
            {
                long parsed;
                if (value.Type != kind || kind == JTokenType.Integer &&
                    !long.TryParse(value.ToString(Formatting.None), NumberStyles.Integer, CultureInfo.InvariantCulture, out parsed))
                    throw new ArgumentException("The " + name + " field has an incompatible JSON kind or Int64 width.", "request");
            }
        }
    }
}
