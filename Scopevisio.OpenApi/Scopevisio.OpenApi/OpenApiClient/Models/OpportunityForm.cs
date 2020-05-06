﻿// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace CompuMaster.Scopevisio.OpenApi.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;

    public partial class OpportunityForm
    {
        /// <summary>
        /// Initializes a new instance of the OpportunityForm class.
        /// </summary>
        public OpportunityForm() { }

        /// <summary>
        /// Initializes a new instance of the OpportunityForm class.
        /// </summary>
        public OpportunityForm(long customerContactId, long documentDate, long? projectId = default(long?), string customerContactName = default(string), string customerContactCustomerNumber = default(string), long? customerPersonalAccountId = default(long?), string customerPersonalAccountNumber = default(string), string customerPersonalAccountName = default(string), string customerPersonalAccountExternalNumber = default(string), string customerName = default(string), string customerNumber = default(string), string customerNameExtra = default(string), string name = default(string), string text = default(string), long? customerContactPerson = default(long?), string documentNumber = default(string), string ourContactPerson = default(string), string deliveryType = default(string), double? deliveryCosts = default(double?), double? cashOnDelivery = default(double?), string textTitle = default(string), string textSubTitle = default(string), string textIntro = default(string), string textExtro = default(string), long? selectedAddress = default(long?), string otherAddress = default(string), bool? gross = default(bool?), long? permissionType = default(long?), string readPermissionProfiles = default(string), string readPermissionLogins = default(string), string writePermissionProfiles = default(string), string writePermissionLogins = default(string), IList<CustomField> customFields = default(IList<CustomField>), Positionsform positionsForm = default(Positionsform), long? deliveryDate = default(long?), long? deliveryDateTo = default(long?), bool? isEndDiscountAbsolute = default(bool?), double? endDiscount = default(double?), string paymentTypeName = default(string), bool? isPaid = default(bool?), long? discountDate1 = default(long?), long? discountDate2 = default(long?), long? dueDate = default(long?), double? discountPercent1 = default(double?), double? discountPercent2 = default(double?), bool? explicitDiscount = default(bool?), double? discountAmount1 = default(double?), double? discountAmount2 = default(double?), string salesPhase = default(string), string salesClassification = default(string), string salesActivity = default(string), string phase = default(string), double? feasibility = default(double?), long? closingDateTs = default(long?), long? reminderDateTs = default(long?), string stateName = default(string))
        {
            ProjectId = projectId;
            CustomerContactId = customerContactId;
            CustomerContactName = customerContactName;
            CustomerContactCustomerNumber = customerContactCustomerNumber;
            CustomerPersonalAccountId = customerPersonalAccountId;
            CustomerPersonalAccountNumber = customerPersonalAccountNumber;
            CustomerPersonalAccountName = customerPersonalAccountName;
            CustomerPersonalAccountExternalNumber = customerPersonalAccountExternalNumber;
            CustomerName = customerName;
            CustomerNumber = customerNumber;
            CustomerNameExtra = customerNameExtra;
            Name = name;
            Text = text;
            CustomerContactPerson = customerContactPerson;
            DocumentDate = documentDate;
            DocumentNumber = documentNumber;
            OurContactPerson = ourContactPerson;
            DeliveryType = deliveryType;
            DeliveryCosts = deliveryCosts;
            CashOnDelivery = cashOnDelivery;
            TextTitle = textTitle;
            TextSubTitle = textSubTitle;
            TextIntro = textIntro;
            TextExtro = textExtro;
            SelectedAddress = selectedAddress;
            OtherAddress = otherAddress;
            Gross = gross;
            PermissionType = permissionType;
            ReadPermissionProfiles = readPermissionProfiles;
            ReadPermissionLogins = readPermissionLogins;
            WritePermissionProfiles = writePermissionProfiles;
            WritePermissionLogins = writePermissionLogins;
            CustomFields = customFields;
            PositionsForm = positionsForm;
            DeliveryDate = deliveryDate;
            DeliveryDateTo = deliveryDateTo;
            IsEndDiscountAbsolute = isEndDiscountAbsolute;
            EndDiscount = endDiscount;
            PaymentTypeName = paymentTypeName;
            IsPaid = isPaid;
            DiscountDate1 = discountDate1;
            DiscountDate2 = discountDate2;
            DueDate = dueDate;
            DiscountPercent1 = discountPercent1;
            DiscountPercent2 = discountPercent2;
            ExplicitDiscount = explicitDiscount;
            DiscountAmount1 = discountAmount1;
            DiscountAmount2 = discountAmount2;
            SalesPhase = salesPhase;
            SalesClassification = salesClassification;
            SalesActivity = salesActivity;
            Phase = phase;
            Feasibility = feasibility;
            ClosingDateTs = closingDateTs;
            ReminderDateTs = reminderDateTs;
            StateName = stateName;
        }

        /// <summary>
        /// projectId (Projekt). Sets also projectNumber, projectName. Please
        /// look up in your sales (Vertriebs-) and/or outgoing invoice
        /// settings (Abrechnungseinstellungen) to check if projectId is
        /// mandatory.
        /// </summary>
        [JsonProperty(PropertyName = "projectId")]
        public long? ProjectId { get; set; }

        /// <summary>
        /// customerContactId (Lead/Interessent/Kunde). Sets also
        /// customerPersonalAccountId, customerPersonalAccountNumber,
        /// customerPersonalAccountName,
        /// customerPersonalAccountExternalNumber, customerName,
        /// customerNameExtra, customerNumber, customerContactName,
        /// customerContactCustomerNumber. Must be set if project is not
        /// mandatory. Ignored otherwise.
        /// </summary>
        [JsonProperty(PropertyName = "customerContactId")]
        public long CustomerContactId { get; set; }

        /// <summary>
        /// customerContactName - the name of the contact with the given
        /// customerContactId.
        /// </summary>
        [JsonProperty(PropertyName = "customerContactName")]
        public string CustomerContactName { get; private set; }

        /// <summary>
        /// The customer number of the contact with the given
        /// customerContactId. Where to find the value in the Scopevisio
        /// client: open contact with master id == customerContactId, go to
        /// sphere (Rolle) Allgemein. The field contact number
        /// (Kontaktnummer) is the customerContactCustomerNumber.
        /// </summary>
        [JsonProperty(PropertyName = "customerContactCustomerNumber")]
        public string CustomerContactCustomerNumber { get; private set; }

        /// <summary>
        /// The master id of personal account associated with the contact with
        /// the given customerContactId.
        /// </summary>
        [JsonProperty(PropertyName = "customerPersonalAccountId")]
        public long? CustomerPersonalAccountId { get; private set; }

        /// <summary>
        /// The number of the personal account associated with the contact
        /// with the given customerContactId. Where to find the value in the
        /// Scopevisio client: open contact with master id ==
        /// customerContactId, go to sphere (Rolle) Debitor. Field account
        /// number (Kontonummer) is the customerPersonalAccountNumber. This
        /// field also corresponds to field "debitorNumber" from endpoint
        /// /contact/{customerContactId} and field  "number" from
        /// /debitoraccounts.
        /// </summary>
        [JsonProperty(PropertyName = "customerPersonalAccountNumber")]
        public string CustomerPersonalAccountNumber { get; private set; }

        /// <summary>
        /// The name of the personal account associated with the contact with
        /// the given customerContactId. Where to find the value in the
        /// Scopevisio client: open contact with master id ==
        /// customerContactId, go to sphere (Rolle) Debitor. Field account
        /// name (Kontoname) is the customerPersonalAccountName. This field
        /// also corresponds to field "name" from endpoint /debitoraccounts.
        /// </summary>
        [JsonProperty(PropertyName = "customerPersonalAccountName")]
        public string CustomerPersonalAccountName { get; private set; }

        /// <summary>
        /// The customer number of the personal account with the given
        /// customerPersonalAccountNumber. Where to find the value in the
        /// Scopevisio client: open contact with master id ==
        /// customerContactId, go to sphere (Rolle) Debitor.  Field customer
        /// number (Kundennummer) is the
        /// customerPersonalAccountExternalNumber.
        /// </summary>
        [JsonProperty(PropertyName = "customerPersonalAccountExternalNumber")]
        public string CustomerPersonalAccountExternalNumber { get; private set; }

        /// <summary>
        /// This field is for internal use only. Please use field
        /// customerContactName.
        /// </summary>
        [JsonProperty(PropertyName = "customerName")]
        public string CustomerName { get; private set; }

        /// <summary>
        /// This field is for internal use only. Please use fields
        /// customerContactCustomerNumber or
        /// customerPersonalAccountExternalNumber.
        /// </summary>
        [JsonProperty(PropertyName = "customerNumber")]
        public string CustomerNumber { get; private set; }

        /// <summary>
        /// This field is for internal use only. Please use fields
        /// customerContactCustomerNumber or customerPersonalAccountName.
        /// </summary>
        [JsonProperty(PropertyName = "customerNameExtra")]
        public string CustomerNameExtra { get; private set; }

        /// <summary>
        /// name (Bezeichnung)
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// text (Beschreibung)
        /// </summary>
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        /// <summary>
        /// customerContactPerson (Ansprechpartner). Sets also
        /// customerContactPersonName. The contact must be an employee of the
        /// project contact (contactId).
        /// </summary>
        [JsonProperty(PropertyName = "customerContactPerson")]
        public long? CustomerContactPerson { get; set; }

        /// <summary>
        /// documentDate (PZ-/Angebots-/Auftrags-/Rechnungs-/GS-/LS-Datum). If
        /// not given, it is set to the current date. Sets also
        /// documentDateMonth.
        /// </summary>
        [JsonProperty(PropertyName = "documentDate")]
        public long DocumentDate { get; set; }

        /// <summary>
        /// documentNumber
        /// (Potenzial-/Angebots-/Auftrags-/Rechnungs-/Gutschrifts-/Lieferscheinnummer).The
        /// field is mandatory. If not provided a document number is
        /// generated.
        /// </summary>
        [JsonProperty(PropertyName = "documentNumber")]
        public string DocumentNumber { get; set; }

        /// <summary>
        /// ourContactPerson (Potenzial-/Angebots-
        /// Auftrags-/Rechnungs-/Gutschrifts-/Lieferscheinbearbeiter). The
        /// UID of a Scopevisio user. The user must have a user contact (Aus
        /// Benutzerkontakt) for the current organisation. Setting this field
        /// also sets ourContactPersonName.
        /// </summary>
        [JsonProperty(PropertyName = "ourContactPerson")]
        public string OurContactPerson { get; set; }

        /// <summary>
        /// deliveryType (Lieferart).
        /// </summary>
        [JsonProperty(PropertyName = "deliveryType")]
        public string DeliveryType { get; set; }

        /// <summary>
        /// deliveryCosts (Lieferkosten).
        /// </summary>
        [JsonProperty(PropertyName = "deliveryCosts")]
        public double? DeliveryCosts { get; set; }

        /// <summary>
        /// cashOnDelivery (Nachnamegebühr).
        /// </summary>
        [JsonProperty(PropertyName = "cashOnDelivery")]
        public double? CashOnDelivery { get; set; }

        /// <summary>
        /// textTitle (Titel).
        /// </summary>
        [JsonProperty(PropertyName = "textTitle")]
        public string TextTitle { get; set; }

        /// <summary>
        /// textSubTitle (Untertitel).
        /// </summary>
        [JsonProperty(PropertyName = "textSubTitle")]
        public string TextSubTitle { get; set; }

        /// <summary>
        /// textIntro (Einleitungstext). Use "\\\\n" to insert new line.
        /// </summary>
        [JsonProperty(PropertyName = "textIntro")]
        public string TextIntro { get; set; }

        /// <summary>
        /// textExtro (Schlusstext). Use "\\\\n" to insert new line.
        /// </summary>
        [JsonProperty(PropertyName = "textExtro")]
        public string TextExtro { get; set; }

        /// <summary>
        /// selectedAddress (Adresse [Haupt-/Rechnungs-/Liefer-/freie
        /// Adresse]). 0 = Hauptadresse, 1 = Rechnungsadresse, 2 = freie
        /// Adresse, 3 = Lieferadresse
        /// </summary>
        [JsonProperty(PropertyName = "selectedAddress")]
        public long? SelectedAddress { get; set; }

        /// <summary>
        /// otherAddress (freie Adresse). Use "\\\\n" to insert new line.
        /// </summary>
        [JsonProperty(PropertyName = "otherAddress")]
        public string OtherAddress { get; set; }

        /// <summary>
        /// gross (Preisangabe - Netto/Brutto)
        /// </summary>
        [JsonProperty(PropertyName = "gross")]
        public bool? Gross { get; set; }

        /// <summary>
        /// 0 = Öffentlich, 1 = Benutzerdefiniert, 2 = Persönlich
        /// </summary>
        [JsonProperty(PropertyName = "permissionType")]
        public long? PermissionType { get; set; }

        /// <summary>
        /// comma separated list of profiles for the read permissions,
        /// evaluated only if permissionType equals 1.
        /// </summary>
        [JsonProperty(PropertyName = "readPermissionProfiles")]
        public string ReadPermissionProfiles { get; set; }

        /// <summary>
        /// comma separated list of user login emails for the read
        /// permissions, evaluated only if permissionType equals 1.
        /// </summary>
        [JsonProperty(PropertyName = "readPermissionLogins")]
        public string ReadPermissionLogins { get; set; }

        /// <summary>
        /// comma separated list of profiles for full access, evaluated only
        /// if permissionType equals 1.
        /// </summary>
        [JsonProperty(PropertyName = "writePermissionProfiles")]
        public string WritePermissionProfiles { get; set; }

        /// <summary>
        /// comma separated list of user login emails, evaluated only if
        /// permissionType equals 1.
        /// </summary>
        [JsonProperty(PropertyName = "writePermissionLogins")]
        public string WritePermissionLogins { get; set; }

        /// <summary>
        /// Benutzerdefinierte Felder
        /// </summary>
        [JsonProperty(PropertyName = "customFields")]
        public IList<CustomField> CustomFields { get; set; }

        /// <summary>
        /// positionsForm
        /// </summary>
        [JsonProperty(PropertyName = "positionsForm")]
        public Positionsform PositionsForm { get; set; }

        /// <summary>
        /// deliveryDate (Leistungsdatum). The deliveryDate as epoch timestamp
        /// in milliseconds.
        /// </summary>
        [JsonProperty(PropertyName = "deliveryDate")]
        public long? DeliveryDate { get; set; }

        /// <summary>
        /// deliveryDateTo (Leistungsdatum-bis). The deliveryDateTo as epoch
        /// timestamp in milliseconds.
        /// </summary>
        [JsonProperty(PropertyName = "deliveryDateTo")]
        public long? DeliveryDateTo { get; set; }

        /// <summary>
        /// isEndDiscountAbsolute (Schlussrabatt), true = absolute, false =
        /// percent
        /// </summary>
        [JsonProperty(PropertyName = "isEndDiscountAbsolute")]
        public bool? IsEndDiscountAbsolute { get; set; }

        /// <summary>
        /// endDiscount(Schlussrabatt), if isEndDiscountAbsolute is set to
        /// true, then endDiscount should be an amount, if
        /// isEndDiscountAbsolute is set to false then a percentage is needed.
        /// </summary>
        [JsonProperty(PropertyName = "endDiscount")]
        public double? EndDiscount { get; set; }

        /// <summary>
        /// paymentTypeName (Zahlungsart). If not provided for new invoice,
        /// the payment type from the personal account (connected to the
        /// customerContactId) is used. Sets also paymentType. Possible
        /// values include: 'Vorkasse', 'Überweisung', 'PayPal', 'Nachname',
        /// 'Einzugsermächtigung', 'Rechnung', 'EC-Karte',
        /// 'Abbuchungsgenehmigung', 'Bar', 'Kreditkarte'
        /// </summary>
        [JsonProperty(PropertyName = "paymentTypeName")]
        public string PaymentTypeName { get; set; }

        /// <summary>
        /// isPaid (Auf exportiertem Beleg als Bezahlt ausweisen)
        /// </summary>
        [JsonProperty(PropertyName = "isPaid")]
        public bool? IsPaid { get; set; }

        /// <summary>
        /// discountDate1 (Skontodatum 1). The discountDate1 as epoch
        /// timestamp in milliseconds. If it is set either discountPercent1
        /// or discountAmount1 must be set. If one of the payment term
        /// properties is provided (discountDate1, discountDate2, dueDate,
        /// discountPercent1, discountPercent2, discountAmount1,
        /// discountAmount2) when creating an invoice, then the form is used.
        /// Otherwise the payment term from the personal account (connected
        /// to the customerContactId) is used.
        /// </summary>
        [JsonProperty(PropertyName = "discountDate1")]
        public long? DiscountDate1 { get; set; }

        /// <summary>
        /// discountDate2 (Skontodatum 2). The discountDate2 as epoch
        /// timestamp in milliseconds. It must be greater than discountDate1.
        /// If it is set either discountPercent2 or discountAmount2 must be
        /// set.
        /// </summary>
        [JsonProperty(PropertyName = "discountDate2")]
        public long? DiscountDate2 { get; set; }

        /// <summary>
        /// dueDate (Fälligkeitsdatum). The dueDate as epoch timestamp in
        /// milliseconds. It must be greater than discountDate2.
        /// </summary>
        [JsonProperty(PropertyName = "dueDate")]
        public long? DueDate { get; set; }

        /// <summary>
        /// discountPercent1 (Skontosatz1). Either discount percent(s) or
        /// discount amount(s) are allowed. Setting both is not possible. If
        /// set discountDate1 must be set too.
        /// </summary>
        [JsonProperty(PropertyName = "discountPercent1")]
        public double? DiscountPercent1 { get; set; }

        /// <summary>
        /// discountPercent2 (Skontosatz2). Either discount percent(s) or
        /// discount amount(s) are allowed. Setting both is not possible. If
        /// set discountDate2 must be set too.
        /// </summary>
        [JsonProperty(PropertyName = "discountPercent2")]
        public double? DiscountPercent2 { get; set; }

        /// <summary>
        /// explicitDiscount (Skontobeträge manuell eingeben). If set
        /// discountAmount(s) must be provided
        /// </summary>
        [JsonProperty(PropertyName = "explicitDiscount")]
        public bool? ExplicitDiscount { get; set; }

        /// <summary>
        /// discountAmount1 (Skontobetrag1). Only evaluated if
        /// explicitDiscount is true. If set discountDate1 must be set too.
        /// </summary>
        [JsonProperty(PropertyName = "discountAmount1")]
        public double? DiscountAmount1 { get; set; }

        /// <summary>
        /// discountAmount2 (Skontobetrag2). Only evaluated if
        /// explicitDiscount is true. If set discountDate2 must be set too.
        /// </summary>
        [JsonProperty(PropertyName = "discountAmount2")]
        public double? DiscountAmount2 { get; set; }

        /// <summary>
        /// salesPhase (Potenzial-/Angebotsphase).  Please check your catalog
        /// in the Scopevisio desktop client for allowable values. Possible
        /// values include: 'Qualifizierungsphase', 'Grundbetreuung',
        /// 'Intensivbetreuung', 'Angebotsbetreuung', 'Preisverhandlung',
        /// 'Abschlussverhandlung', 'unverbindliche Zusage (z.B. mündliche
        /// Zusage)', 'verbindliche Zusage (Auftrag', 'Vertrag)'
        /// </summary>
        [JsonProperty(PropertyName = "salesPhase")]
        public string SalesPhase { get; set; }

        /// <summary>
        /// salesClassification (Angebotspriorität).  Please check your
        /// catalog in the Scopevisio desktop client for allowable values.
        /// Possible values include: 'VIP Priorität (sehr wichtig und sehr
        /// dringlich)', 'A Priorität (wichtig und dringlich)', 'B Priorität
        /// (wichtig und nicht dringlich)', 'C Priorität (nicht wichtig und
        /// dringlich)', 'D Priorität (nicht wichtig und nicht dringlich)',
        /// 'E Priorität (direkt verwerfen)'
        /// </summary>
        [JsonProperty(PropertyName = "salesClassification")]
        public string SalesClassification { get; set; }

        /// <summary>
        /// salesActivity (Folgeaktivität).  Please check your catalog in the
        /// Scopevisio desktop client for allowable values. Possible values
        /// include: '1. Anruf', '2. Anruf', '3. Anruf', 'zurückrufen',
        /// 'E-Mail senden', 'Webinar abhalten', 'Prospekte zusenden',
        /// 'Angebot erläutern'
        /// </summary>
        [JsonProperty(PropertyName = "salesActivity")]
        public string SalesActivity { get; set; }

        /// <summary>
        /// phase (Verkaufswahrscheinlichkeit).  Sets also phaseId. Please
        /// check your catalog in the Scopevisio desktop client for allowable
        /// values. Possible values include: 'Auftrag wurde verbindlich
        /// erteilt', 'Vorvertrag wurde geschlossen', 'Abschlussverhandlungen
        /// führen', 'Interessent erteilt mündliche Zusage', 'Interessent
        /// favorisiert unser Angebot', 'Entscheidung herbeiführen',
        /// 'Interessent signalisiert deutliches Interesse',
        /// 'Nutzenargumentation aufbauen', 'Angebot abgegeben', 'Angebot in
        /// Abstimmung', 'Potenzial qualifizieren', 'Potenzial
        /// identifizieren', 'Individuelle Vertriebsaktivität gestartet',
        /// 'Bedarf qualifiziert', 'Bedarf identifiziert', 'keine Prognose',
        /// 'ungewichtet', 'verloren'
        /// </summary>
        [JsonProperty(PropertyName = "phase")]
        public string Phase { get; set; }

        /// <summary>
        /// feasibility (Verkaufswahrscheinlichkeit %)
        /// </summary>
        [JsonProperty(PropertyName = "feasibility")]
        public double? Feasibility { get; set; }

        /// <summary>
        /// closingDateTs (Entscheidungsdatum). Sets also closingDateMonth The
        /// closingDateTs as epoch timestamp in milliseconds.
        /// </summary>
        [JsonProperty(PropertyName = "closingDateTs")]
        public long? ClosingDateTs { get; set; }

        /// <summary>
        /// reminderDateTs (Wiedervorlage). The reminderDateTs as epoch
        /// timestamp in milliseconds.
        /// </summary>
        [JsonProperty(PropertyName = "reminderDateTs")]
        public long? ReminderDateTs { get; set; }

        /// <summary>
        /// state (PZ-Status). Sets also state. Possible values include:
        /// 'identifiziert', 'erstellt', 'qualifiziert', 'überführt',
        /// 'verworfen'
        /// </summary>
        [JsonProperty(PropertyName = "stateName")]
        public string StateName { get; set; }

        /// <summary>
        /// Validate the object. Throws ValidationException if validation fails.
        /// </summary>
        public virtual void Validate()
        {
            if (this.CustomFields != null)
            {
                foreach (var element in this.CustomFields)
                {
                    if (element != null)
                    {
                        element.Validate();
                    }
                }
            }
            if (this.PositionsForm != null)
            {
                this.PositionsForm.Validate();
            }
        }
    }
}
