using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi
{
    /// <summary>
    /// Client for accessing the OpenScope API at https://appload.scopevisio.com/rest/swagger.json
    /// </summary>
    public class OpenScopeApiClient
    {
        /// <summary>
        /// Create a new client instance for the OpenScope API REST web service at https://appload.scopevisio.com/rest/swagger.json
        /// </summary>
        public OpenScopeApiClient() : this(Client.Configuration.Default)
        {
        }

        /// <summary>
        /// Create a new client instance for the OpenScope API REST web service at https://appload.scopevisio.com/rest/swagger.json
        /// </summary>
        /// <param name="config"></param>
        public OpenScopeApiClient(Client.Configuration config)
        {
            this.Config = config;
            this.AuthorizationApi = new Api.AuthorizationApi(this.Config);
            this.AdditionalApi = new Api.AdditionalApi(this.Config);
            this.AccountingApi = new Api.AccountingApi(this.Config);
            this.ActivitiesApi = new Api.ActivitiesApi(this.Config);
            this.CashbookApi = new Api.CashbookApi(this.Config);
            this.ContactApi = new Api.ContactApi(this.Config);
            this.CreditApi = new Api.CreditApi(this.Config);
            this.DispatchApi = new Api.DispatchApi(this.Config);
            this.HumanResourcesApi = new Api.HumanResourcesApi(this.Config);
            this.IncomingInvoiceApi = new Api.IncomingInvoiceApi(this.Config);
            this.OfferApi = new Api.OfferApi(this.Config);
            this.OpportunityApi = new Api.OpportunityApi(this.Config);
            this.OrderApi = new Api.OrderApi(this.Config);
            this.OrganisationApi = new Api.OrganisationApi(this.Config);
            this.OutgoingInvoiceApi = new Api.OutgoingInvoiceApi(this.Config);
            this.ProductApi = new Api.ProductApi(this.Config);
            this.ProjectApi = new Api.ProjectApi(this.Config);
            this.RecurringInvoiceApi = new Api.RecurringInvoiceApi(this.Config);
            this.ReportApi = new Api.ReportApi(this.Config);
            this.TaskApi = new Api.TaskApi(this.Config);
            this.TeamworkApi = new Api.TeamworkApi(this.Config);
            this.UserApi = new Api.UserApi(this.Config);
        }

        /// <summary>
        /// Configuration for accessing OpenScope API
        /// </summary>
        public Client.Configuration Config { get; set; }

        /// <summary>
        /// Client token for accessing OpenScope API
        /// </summary>
        public Model.TokenResponse Token { get; set; }

        /// <summary>
        /// Authorization API methods
        /// </summary>
        public Api.AuthorizationApi AuthorizationApi { get; set; }

        /// <summary>
        /// Additional API methods
        /// </summary>
        public Api.AdditionalApi AdditionalApi { get; set; }

        /// <summary>
        /// Accounting API methods
        /// </summary>
        public Api.AccountingApi AccountingApi { get; set; }

        /// <summary>
        /// Activities API methods
        /// </summary>
        public Api.ActivitiesApi ActivitiesApi { get; set; }

        /// <summary>
        /// Cashbook API methods
        /// </summary>
        public Api.CashbookApi CashbookApi { get; set; }

        /// <summary>
        /// Contact API methods
        /// </summary>
        public Api.ContactApi ContactApi { get; set; }

        /// <summary>
        /// Credit API methods
        /// </summary>
        public Api.CreditApi CreditApi { get; set; }

        /// <summary>
        /// Dispatch API methods
        /// </summary>
        public Api.DispatchApi DispatchApi { get; set; }

        /// <summary>
        /// Human resources API methods
        /// </summary>
        public Api.HumanResourcesApi HumanResourcesApi { get; set; }

        /// <summary>
        /// Incoming invoice API methods
        /// </summary>
        public Api.IncomingInvoiceApi IncomingInvoiceApi { get; set; }

        /// <summary>
        /// Offer API methods
        /// </summary>
        public Api.OfferApi OfferApi { get; set; }

        /// <summary>
        /// Opportunity API methods
        /// </summary>
        public Api.OpportunityApi OpportunityApi { get; set; }

        /// <summary>
        /// Order API methods
        /// </summary>
        public Api.OrderApi OrderApi { get; set; }

        /// <summary>
        /// Organization API methods
        /// </summary>
        public Api.OrganisationApi OrganisationApi { get; set; }

        /// <summary>
        /// Outgoing invoice API methods
        /// </summary>
        public Api.OutgoingInvoiceApi OutgoingInvoiceApi { get; set; }

        /// <summary>
        /// Product API methods
        /// </summary>
        public Api.ProductApi ProductApi { get; set; }

        /// <summary>
        /// Project API methods
        /// </summary>
        public Api.ProjectApi ProjectApi { get; set; }

        /// <summary>
        /// Recurring invoice API methods
        /// </summary>
        public Api.RecurringInvoiceApi RecurringInvoiceApi { get; set; }

        /// <summary>
        /// Report API methods
        /// </summary>
        public Api.ReportApi ReportApi { get; set; }

        /// <summary>
        /// Task API methods
        /// </summary>
        public Api.TaskApi TaskApi { get; set; }

        /// <summary>
        /// Teamwork API methods
        /// </summary>
        public Api.TeamworkApi TeamworkApi { get; set; }

        /// <summary>
        /// User API methods
        /// </summary>
        public Api.UserApi UserApi { get; set; }

        /// <summary>
        /// Authorize user with username, customer number, organisation name and password as set up in configuration
        /// </summary>
        public void AuthorizeWithToken(Model.TokenResponse token)
        {
            this.Token = token;
        }

        /// <summary>
        /// Authorize user with username, customer number, organisation name and password as set up in configuration
        /// </summary>
        public void AuthorizeWithUserCredentials()
        {
            this.Token = this.AuthorizationApi.TokenWithHttpInfo("password", this.Config.ClientNumber, null, null, this.Config.Username, null, this.Config.OrganisationName, this.Config.Password, null, null, null, null).Data;
        }

        /// <summary>
        /// Update configuration and authorize user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="customerNo"></param>
        /// <param name="password"></param>
        public void AuthorizeWithUserCredentials(string username, string customerNo, string password)
        {
            this.Config.Username = username;
            this.Config.ClientNumber = customerNo;
            this.Config.Password = password;
            this.AuthorizeWithUserCredentials();
        }

        /// <summary>
        /// Update configuration and authorize user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="customerNo"></param>
        /// <param name="organisationName"></param>
        /// <param name="password"></param>
        public void AuthorizeWithUserCredentials(string username, string customerNo, string organisationName, string password)
        {
            this.Config.Username = username;
            this.Config.ClientNumber = customerNo;
            this.Config.OrganisationName = organisationName;
            this.Config.Password = password;
            this.AuthorizeWithUserCredentials();
        }
    }
}
