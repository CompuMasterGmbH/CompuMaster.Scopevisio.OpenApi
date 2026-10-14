using Newtonsoft.Json.Linq;
using System;
using System.Globalization;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies explicitly supplied current contact creation or update fields.</summary>
    /// <remarks>The complete JSON object is deeply copied before sending. Typed null setters omit fields; raw JSON null remains available for updates. Creation requires lastname and person in a supplied object. Catalogue strings, custom data and timestamps remain unchanged for server validation. Legacy KontaktForm behavior is preserved.</remarks>
    public class ContactWriteRequest : JObject
    {
        /// <summary>Initializes an empty contact request without inserting defaults.</summary>
        public ContactWriteRequest() { }

        /// <summary>Initializes a request by deeply copying complete contact JSON.</summary>
        /// <param name="other">The original object, including unknown fields.</param>
        /// <exception cref="ArgumentNullException">The source object is null.</exception>
        public ContactWriteRequest(JObject other) : base(other ?? throw new ArgumentNullException(nameof(other))) { }

        /// <summary>Gets or sets the original person value.</summary>
        /// <value>The explicit required creation choice: true for a person and false for an organisation. Null omits it; the server evaluates this field only for creation.</value>
        public bool? Person { get { return Value<bool?>("person"); } set { SetOptional("person", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original salutation value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Salutation { get { return Value<string>("salutation"); } set { SetOptional("salutation", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original title value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Title { get { return Value<string>("title"); } set { SetOptional("title", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original lastname value.</summary>
        /// <value>The required string when creating with a supplied object; null omits it for partial updates. Empty or whitespace text is not normalized.</value>
        public string Lastname { get { return Value<string>("lastname"); } set { SetOptional("lastname", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original firstname value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Firstname { get { return Value<string>("firstname"); } set { SetOptional("firstname", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original paymentTypeName value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string PaymentTypeName { get { return Value<string>("paymentTypeName"); } set { SetOptional("paymentTypeName", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original federalState value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string FederalState { get { return Value<string>("federalState"); } set { SetOptional("federalState", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original federalState2 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string FederalState2 { get { return Value<string>("federalState2"); } set { SetOptional("federalState2", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original federalState3 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string FederalState3 { get { return Value<string>("federalState3"); } set { SetOptional("federalState3", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original federalState4 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string FederalState4 { get { return Value<string>("federalState4"); } set { SetOptional("federalState4", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original federalState5 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string FederalState5 { get { return Value<string>("federalState5"); } set { SetOptional("federalState5", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original federalState6 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string FederalState6 { get { return Value<string>("federalState6"); } set { SetOptional("federalState6", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original street1 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Street1 { get { return Value<string>("street1"); } set { SetOptional("street1", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original street2 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Street2 { get { return Value<string>("street2"); } set { SetOptional("street2", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original street3 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Street3 { get { return Value<string>("street3"); } set { SetOptional("street3", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original street4 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Street4 { get { return Value<string>("street4"); } set { SetOptional("street4", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original street5 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Street5 { get { return Value<string>("street5"); } set { SetOptional("street5", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original street6 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Street6 { get { return Value<string>("street6"); } set { SetOptional("street6", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original addressExtra1 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string AddressExtra1 { get { return Value<string>("addressExtra1"); } set { SetOptional("addressExtra1", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original addressExtra2 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string AddressExtra2 { get { return Value<string>("addressExtra2"); } set { SetOptional("addressExtra2", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original addressExtra3 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string AddressExtra3 { get { return Value<string>("addressExtra3"); } set { SetOptional("addressExtra3", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original addressExtra4 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string AddressExtra4 { get { return Value<string>("addressExtra4"); } set { SetOptional("addressExtra4", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original addressExtra5 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string AddressExtra5 { get { return Value<string>("addressExtra5"); } set { SetOptional("addressExtra5", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original addressExtra6 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string AddressExtra6 { get { return Value<string>("addressExtra6"); } set { SetOptional("addressExtra6", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original city1 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string City1 { get { return Value<string>("city1"); } set { SetOptional("city1", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original city2 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string City2 { get { return Value<string>("city2"); } set { SetOptional("city2", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original city3 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string City3 { get { return Value<string>("city3"); } set { SetOptional("city3", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original city4 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string City4 { get { return Value<string>("city4"); } set { SetOptional("city4", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original city5 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string City5 { get { return Value<string>("city5"); } set { SetOptional("city5", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original city6 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string City6 { get { return Value<string>("city6"); } set { SetOptional("city6", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original postcode1 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Postcode1 { get { return Value<string>("postcode1"); } set { SetOptional("postcode1", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original postcode2 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Postcode2 { get { return Value<string>("postcode2"); } set { SetOptional("postcode2", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original postcode3 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Postcode3 { get { return Value<string>("postcode3"); } set { SetOptional("postcode3", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original postcode4 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Postcode4 { get { return Value<string>("postcode4"); } set { SetOptional("postcode4", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original postcode5 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Postcode5 { get { return Value<string>("postcode5"); } set { SetOptional("postcode5", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original postcode6 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Postcode6 { get { return Value<string>("postcode6"); } set { SetOptional("postcode6", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original country1 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Country1 { get { return Value<string>("country1"); } set { SetOptional("country1", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original country2 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Country2 { get { return Value<string>("country2"); } set { SetOptional("country2", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original country3 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Country3 { get { return Value<string>("country3"); } set { SetOptional("country3", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original country4 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Country4 { get { return Value<string>("country4"); } set { SetOptional("country4", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original country5 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Country5 { get { return Value<string>("country5"); } set { SetOptional("country5", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original country6 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Country6 { get { return Value<string>("country6"); } set { SetOptional("country6", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original email value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Email { get { return Value<string>("email"); } set { SetOptional("email", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original email2 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Email2 { get { return Value<string>("email2"); } set { SetOptional("email2", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original email3 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Email3 { get { return Value<string>("email3"); } set { SetOptional("email3", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original email4 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Email4 { get { return Value<string>("email4"); } set { SetOptional("email4", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original email5 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Email5 { get { return Value<string>("email5"); } set { SetOptional("email5", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original email6 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Email6 { get { return Value<string>("email6"); } set { SetOptional("email6", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original phone value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Phone { get { return Value<string>("phone"); } set { SetOptional("phone", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original phone2 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Phone2 { get { return Value<string>("phone2"); } set { SetOptional("phone2", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original phone3 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Phone3 { get { return Value<string>("phone3"); } set { SetOptional("phone3", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original phone4 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Phone4 { get { return Value<string>("phone4"); } set { SetOptional("phone4", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original phone5 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Phone5 { get { return Value<string>("phone5"); } set { SetOptional("phone5", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original phone6 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Phone6 { get { return Value<string>("phone6"); } set { SetOptional("phone6", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original mobile value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Mobile { get { return Value<string>("mobile"); } set { SetOptional("mobile", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original mobile2 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Mobile2 { get { return Value<string>("mobile2"); } set { SetOptional("mobile2", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original mobile3 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Mobile3 { get { return Value<string>("mobile3"); } set { SetOptional("mobile3", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original mobile4 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Mobile4 { get { return Value<string>("mobile4"); } set { SetOptional("mobile4", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original mobile5 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Mobile5 { get { return Value<string>("mobile5"); } set { SetOptional("mobile5", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original mobile6 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Mobile6 { get { return Value<string>("mobile6"); } set { SetOptional("mobile6", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original fax value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Fax { get { return Value<string>("fax"); } set { SetOptional("fax", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original fax2 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Fax2 { get { return Value<string>("fax2"); } set { SetOptional("fax2", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original fax3 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Fax3 { get { return Value<string>("fax3"); } set { SetOptional("fax3", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original fax4 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Fax4 { get { return Value<string>("fax4"); } set { SetOptional("fax4", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original fax5 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Fax5 { get { return Value<string>("fax5"); } set { SetOptional("fax5", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original fax6 value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Fax6 { get { return Value<string>("fax6"); } set { SetOptional("fax6", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original preferredKontaktType value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string PreferredKontaktType { get { return Value<string>("preferredKontaktType"); } set { SetOptional("preferredKontaktType", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original preferredInvoiceSendMethod value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string PreferredInvoiceSendMethod { get { return Value<string>("preferredInvoiceSendMethod"); } set { SetOptional("preferredInvoiceSendMethod", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original language value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Language { get { return Value<string>("language"); } set { SetOptional("language", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original position value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Position { get { return Value<string>("position"); } set { SetOptional("position", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original positionReplacement value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string PositionReplacement { get { return Value<string>("positionReplacement"); } set { SetOptional("positionReplacement", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original department value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Department { get { return Value<string>("department"); } set { SetOptional("department", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original industry value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Industry { get { return Value<string>("industry"); } set { SetOptional("industry", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original organisationType value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string OrganisationType { get { return Value<string>("organisationType"); } set { SetOptional("organisationType", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original tradeRegisterName value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string TradeRegisterName { get { return Value<string>("tradeRegisterName"); } set { SetOptional("tradeRegisterName", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original tradeRegisterNumber value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string TradeRegisterNumber { get { return Value<string>("tradeRegisterNumber"); } set { SetOptional("tradeRegisterNumber", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original customerNumber value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string CustomerNumber { get { return Value<string>("customerNumber"); } set { SetOptional("customerNumber", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original customerPersonId value.</summary>
        /// <value>The optional original signed Int64 value, or null to omit it; server identity and range semantics remain authoritative.</value>
        public long? CustomerPersonId { get { return Value<long?>("customerPersonId"); } set { SetOptional("customerPersonId", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original legacyNumber value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string LegacyNumber { get { return Value<string>("legacyNumber"); } set { SetOptional("legacyNumber", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original tags value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Tags { get { return Value<string>("tags"); } set { SetOptional("tags", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original website value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Website { get { return Value<string>("website"); } set { SetOptional("website", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original description value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Description { get { return Value<string>("description"); } set { SetOptional("description", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original vatId value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string VatId { get { return Value<string>("vatId"); } set { SetOptional("vatId", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original currency value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string Currency { get { return Value<string>("currency"); } set { SetOptional("currency", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original customFields value.</summary>
        /// <value>The complete optional array of objects. Assignment copies its contents; null omits the field. Unknown nested data remains available.</value>
        public JArray CustomFields { get { return this["customFields"] as JArray; } set { SetOptional("customFields", value?.DeepClone()); } }

        /// <summary>Gets or sets the original employers value.</summary>
        /// <value>The complete optional array of objects. Assignment copies its contents; null omits the field. Unknown nested data remains available.</value>
        public JArray Employers { get { return this["employers"] as JArray; } set { SetOptional("employers", value?.DeepClone()); } }

        /// <summary>Gets or sets the original deliveryTermIncoterm value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string DeliveryTermIncoterm { get { return Value<string>("deliveryTermIncoterm"); } set { SetOptional("deliveryTermIncoterm", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original deliveryTermIncotermLocation value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string DeliveryTermIncotermLocation { get { return Value<string>("deliveryTermIncotermLocation"); } set { SetOptional("deliveryTermIncotermLocation", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original personalDataType value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        /// <remarks>The server enforces the additional Personal profile and current catalogue or employment constraints.</remarks>
        public string PersonalDataType { get { return Value<string>("personalDataType"); } set { SetOptional("personalDataType", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original personalDataStart value.</summary>
        /// <value>The optional original epoch milliseconds, or null to omit it. The declared first-day-of-month rule is server-authoritative; the client does not convert or normalize timestamps.</value>
        public long? PersonalDataStart { get { return Value<long?>("personalDataStart"); } set { SetOptional("personalDataStart", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original personalDataEnd value.</summary>
        /// <value>The optional original epoch milliseconds, or null to omit it. The declared first-day-of-month rule is server-authoritative; the client does not convert or normalize timestamps.</value>
        public long? PersonalDataEnd { get { return Value<long?>("personalDataEnd"); } set { SetOptional("personalDataEnd", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original personalDataInsuranceType value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        /// <remarks>The server enforces the additional Personal profile and current catalogue or employment constraints.</remarks>
        public string PersonalDataInsuranceType { get { return Value<string>("personalDataInsuranceType"); } set { SetOptional("personalDataInsuranceType", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original personalDataInsurance value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        /// <remarks>The server enforces the additional Personal profile and current catalogue or employment constraints.</remarks>
        public string PersonalDataInsurance { get { return Value<string>("personalDataInsurance"); } set { SetOptional("personalDataInsurance", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original personalDataInsuranceNumber value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        /// <remarks>The server enforces the additional Personal profile and current catalogue or employment constraints.</remarks>
        public string PersonalDataInsuranceNumber { get { return Value<string>("personalDataInsuranceNumber"); } set { SetOptional("personalDataInsuranceNumber", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original personalDataCareInsurance value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        /// <remarks>The server enforces the additional Personal profile and current catalogue or employment constraints.</remarks>
        public string PersonalDataCareInsurance { get { return Value<string>("personalDataCareInsurance"); } set { SetOptional("personalDataCareInsurance", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original personalDataCareInsuranceNumber value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        /// <remarks>The server enforces the additional Personal profile and current catalogue or employment constraints.</remarks>
        public string PersonalDataCareInsuranceNumber { get { return Value<string>("personalDataCareInsuranceNumber"); } set { SetOptional("personalDataCareInsuranceNumber", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original personalDataAnnuityInsurance value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        /// <remarks>The server enforces the additional Personal profile and current catalogue or employment constraints.</remarks>
        public string PersonalDataAnnuityInsurance { get { return Value<string>("personalDataAnnuityInsurance"); } set { SetOptional("personalDataAnnuityInsurance", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original ownerUid value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string OwnerUid { get { return Value<string>("ownerUid"); } set { SetOptional("ownerUid", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original xingLink value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string XingLink { get { return Value<string>("xingLink"); } set { SetOptional("xingLink", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original linkedInLink value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string LinkedInLink { get { return Value<string>("linkedInLink"); } set { SetOptional("linkedInLink", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original facebookLink value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string FacebookLink { get { return Value<string>("facebookLink"); } set { SetOptional("facebookLink", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original permissionType value.</summary>
        /// <value>The optional original signed Int32 value, or null to omit it; server identity and range semantics remain authoritative.</value>
        /// <remarks>The contract declares 0 for public, 1 for custom and 2 for personal permissions. The client checks Int32 width; the server enforces the 0 through 2 semantic range without a default being inserted.</remarks>
        public int? PermissionType { get { return Value<int?>("permissionType"); } set { SetOptional("permissionType", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original readPermissionProfiles value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string ReadPermissionProfiles { get { return Value<string>("readPermissionProfiles"); } set { SetOptional("readPermissionProfiles", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original readPermissionLogins value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string ReadPermissionLogins { get { return Value<string>("readPermissionLogins"); } set { SetOptional("readPermissionLogins", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original writePermissionProfiles value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string WritePermissionProfiles { get { return Value<string>("writePermissionProfiles"); } set { SetOptional("writePermissionProfiles", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original writePermissionLogins value.</summary>
        /// <value>The optional original string, including empty text, or null to omit it. The server validates formats and catalogue values.</value>
        public string WritePermissionLogins { get { return Value<string>("writePermissionLogins"); } set { SetOptional("writePermissionLogins", value != null ? new JValue(value) : null); } }

        /// <summary>Gets or sets the original sphereEmployee value.</summary>
        /// <value>The optional explicit boolean, or null to omit it without inserting false.</value>
        public bool? SphereEmployee { get { return Value<bool?>("sphereEmployee"); } set { SetOptional("sphereEmployee", value.HasValue ? new JValue(value.Value) : null); } }

        /// <summary>Gets or sets the original sphereWorker value.</summary>
        /// <value>The optional explicit boolean, or null to omit it without inserting false.</value>
        public bool? SphereWorker { get { return Value<bool?>("sphereWorker"); } set { SetOptional("sphereWorker", value.HasValue ? new JValue(value.Value) : null); } }

        private void SetOptional(string name, JToken value)
        {
            if (value == null) Remove(name);
            else this[name] = value;
        }

        internal string SnapshotForSend(bool creating)
        {
            var snapshot = (JObject)DeepClone();
            foreach (var name in new[] { "salutation", "title", "lastname", "firstname", "paymentTypeName", "federalState", "federalState2", "federalState3", "federalState4", "federalState5", "federalState6", "street1", "street2", "street3", "street4", "street5", "street6", "addressExtra1", "addressExtra2", "addressExtra3", "addressExtra4", "addressExtra5", "addressExtra6", "city1", "city2", "city3", "city4", "city5", "city6", "postcode1", "postcode2", "postcode3", "postcode4", "postcode5", "postcode6", "country1", "country2", "country3", "country4", "country5", "country6", "email", "email2", "email3", "email4", "email5", "email6", "phone", "phone2", "phone3", "phone4", "phone5", "phone6", "mobile", "mobile2", "mobile3", "mobile4", "mobile5", "mobile6", "fax", "fax2", "fax3", "fax4", "fax5", "fax6", "preferredKontaktType", "preferredInvoiceSendMethod", "language", "position", "positionReplacement", "department", "industry", "organisationType", "tradeRegisterName", "tradeRegisterNumber", "customerNumber", "legacyNumber", "tags", "website", "description", "vatId", "currency", "deliveryTermIncoterm", "deliveryTermIncotermLocation", "personalDataType", "personalDataInsuranceType", "personalDataInsurance", "personalDataInsuranceNumber", "personalDataCareInsurance", "personalDataCareInsuranceNumber", "personalDataAnnuityInsurance", "ownerUid", "xingLink", "linkedInLink", "facebookLink", "readPermissionProfiles", "readPermissionLogins", "writePermissionProfiles", "writePermissionLogins" })
                ValidateKind(snapshot[name], name, JTokenType.String, creating);
            foreach (var name in new[] { "person", "sphereEmployee", "sphereWorker" })
                ValidateKind(snapshot[name], name, JTokenType.Boolean, creating);
            foreach (var name in new[] { "customFields", "employers" })
                ValidateKind(snapshot[name], name, JTokenType.Array, creating);
            foreach (var name in new[] { "customerPersonId", "personalDataStart", "personalDataEnd" })
                ValidateInteger(snapshot[name], name, true, creating);
            foreach (var name in new[] { "permissionType" })
                ValidateInteger(snapshot[name], name, false, creating);
            if (creating && (snapshot["lastname"]?.Type != JTokenType.String || snapshot["person"]?.Type != JTokenType.Boolean))
                throw new ArgumentException("A supplied creation object requires explicit lastname and person fields.", "request");
            ValidateArrays(snapshot, creating);
            return snapshot.ToString(Newtonsoft.Json.Formatting.None);
        }

        private static void ValidateKind(JToken value, string name, JTokenType kind, bool creating)
        {
            if (value == null || (!creating && value.Type == JTokenType.Null)) return;
            if (value.Type != kind) throw new ArgumentException("The " + name + " field has an incompatible JSON kind.", "request");
        }

        private static void ValidateInteger(JToken value, string name, bool wide, bool creating)
        {
            if (value == null || (!creating && value.Type == JTokenType.Null)) return;
            long number;
            if (value.Type != JTokenType.Integer || !long.TryParse(value.ToString(Newtonsoft.Json.Formatting.None), NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out number) ||
                (!wide && (number < int.MinValue || number > int.MaxValue)))
                throw new ArgumentException("The " + name + " field must be a JSON integer within its declared width.", "request");
        }

        private static void ValidateArrays(JObject snapshot, bool creating)
        {
            foreach (var name in new[] { "customFields", "employers" })
            {
                if (!(snapshot[name] is JArray items)) continue;
                foreach (var item in items)
                {
                    if (!(item is JObject row)) throw new ArgumentException("The " + name + " items must be JSON objects.", "request");
                    if (name == "customFields")
                    {
                        if (row["customFieldName"]?.Type != JTokenType.String || row.Property("customFieldValue") == null || (creating && row["customFieldValue"].Type == JTokenType.Null))
                            throw new ArgumentException("A custom field requires its original string name and explicit value.", "request");
                        // The vendor object schema contradicts its scalar examples and general documentation.
                        // Retain mixed JSON custom values, including update nulls, without catalogue/type conversion.
                    }
                    else
                    {
                        ValidateInteger(row["employerId"], "employerId", true, creating);
                        ValidateInteger(row["validFrom"], "validFrom", true, creating);
                        ValidateInteger(row["validTo"], "validTo", true, creating);
                        foreach (var field in new[] { "legacyNumber", "companyType" }) ValidateKind(row[field], field, JTokenType.String, creating);
                        foreach (var field in new[] { "companyDocumentVisibility", "companyCommentVisibility", "companyTaskVisibility", "companyInfoVisibility" })
                            ValidateKind(row[field], field, JTokenType.Boolean, creating);
                        if (row["employerId"]?.Type != JTokenType.Integer && row["legacyNumber"]?.Type != JTokenType.String)
                            throw new ArgumentException("An employer requires an explicit employerId or legacyNumber.", "request");
                    }
                }
            }
        }
    }
}
