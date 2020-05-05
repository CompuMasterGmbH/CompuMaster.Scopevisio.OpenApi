
# Org.OpenAPITools.Model.OrderForm

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**ProjectId** | **long** | projectId (Projekt). Sets also projectNumber, projectName. Please look up in your sales (Vertriebs-) and/or outgoing invoice settings (Abrechnungseinstellungen) to check if projectId is mandatory. | [optional] 
**CustomerContactId** | **long** | customerContactId (Lead/Interessent/Kunde). Sets also customerPersonalAccountId, customerPersonalAccountNumber, customerPersonalAccountName, customerPersonalAccountExternalNumber, customerName, customerNameExtra, customerNumber, customerContactName, customerContactCustomerNumber. Must be set if project is not mandatory. Ignored otherwise. | 
**CustomerContactName** | **string** | customerContactName - the name of the contact with the given customerContactId. | [optional] [readonly] 
**CustomerContactCustomerNumber** | **string** | The customer number of the contact with the given customerContactId. Where to find the value in the Scopevisio client: open contact with master id &#x3D;&#x3D; customerContactId, go to sphere (Rolle) Allgemein. The field contact number (Kontaktnummer) is the customerContactCustomerNumber. | [optional] [readonly] 
**CustomerPersonalAccountId** | **long** | The master id of personal account associated with the contact with the given customerContactId. | [optional] [readonly] 
**CustomerPersonalAccountNumber** | **string** | The number of the personal account associated with the contact with the given customerContactId. Where to find the value in the Scopevisio client: open contact with master id &#x3D;&#x3D; customerContactId, go to sphere (Rolle) Debitor. Field account number (Kontonummer) is the customerPersonalAccountNumber. This field also corresponds to field \&quot;debitorNumber\&quot; from endpoint  /contact/{customerContactId} and field  \&quot;number\&quot; from /debitoraccounts. | [optional] [readonly] 
**CustomerPersonalAccountName** | **string** | The name of the personal account associated with the contact with the given customerContactId. Where to find the value in the Scopevisio client: open contact with master id &#x3D;&#x3D; customerContactId, go to sphere (Rolle) Debitor. Field account name (Kontoname) is the customerPersonalAccountName. This field also corresponds to field \&quot;name\&quot; from endpoint /debitoraccounts. | [optional] [readonly] 
**CustomerPersonalAccountExternalNumber** | **string** | The customer number of the personal account with the given customerPersonalAccountNumber. Where to find the value in the Scopevisio client: open contact with master id &#x3D;&#x3D; customerContactId, go to sphere (Rolle) Debitor.  Field customer number (Kundennummer) is the customerPersonalAccountExternalNumber. | [optional] [readonly] 
**CustomerName** | **string** | This field is for internal use only. Please use field customerContactName. | [optional] [readonly] 
**CustomerNumber** | **string** | This field is for internal use only. Please use fields customerContactCustomerNumber or customerPersonalAccountExternalNumber. | [optional] [readonly] 
**CustomerNameExtra** | **string** | This field is for internal use only. Please use fields customerContactCustomerNumber or customerPersonalAccountName. | [optional] [readonly] 
**Name** | **string** | name (Bezeichnung) | [optional] 
**Text** | **string** | text (Beschreibung) | [optional] 
**CustomerContactPerson** | **long** | customerContactPerson (Ansprechpartner). Sets also customerContactPersonName. The contact must be an employee of the project contact (contactId). | [optional] 
**DocumentDate** | **long** | documentDate (PZ-/Angebots-/Auftrags-/Rechnungs-/GS-/LS-Datum). If not given, it is set to the current date. Sets also documentDateMonth. | 
**DocumentNumber** | **string** | documentNumber (Potenzial-/Angebots-/Auftrags-/Rechnungs-/Gutschrifts-/Lieferscheinnummer).The field is mandatory. If not provided a document number is generated. | [optional] 
**OurContactPerson** | **string** | ourContactPerson (Potenzial-/Angebots- Auftrags-/Rechnungs-/Gutschrifts-/Lieferscheinbearbeiter). The UID of a Scopevisio user. The user must have a user contact (Aus Benutzerkontakt) for the current organisation. Setting this field also sets ourContactPersonName. | [optional] 
**DeliveryType** | **string** | deliveryType (Lieferart). | [optional] 
**DeliveryCosts** | **decimal** | deliveryCosts (Lieferkosten). | [optional] 
**CashOnDelivery** | **decimal** | cashOnDelivery (Nachnamegebühr). | [optional] 
**TextTitle** | **string** | textTitle (Titel). | [optional] 
**TextSubTitle** | **string** | textSubTitle (Untertitel). | [optional] 
**TextIntro** | **string** | textIntro (Einleitungstext). Use \&quot;\\n\&quot; to insert new line. | [optional] 
**TextExtro** | **string** | textExtro (Schlusstext). Use \&quot;\\n\&quot; to insert new line. | [optional] 
**SelectedAddress** | **long** | selectedAddress (Adresse [Haupt-/Rechnungs-/Liefer-/freie Adresse]). 0 &#x3D; Hauptadresse, 1 &#x3D; Rechnungsadresse, 2 &#x3D; freie Adresse, 3 &#x3D; Lieferadresse | [optional] 
**OtherAddress** | **string** | otherAddress (freie Adresse). Use \&quot;\\n\&quot; to insert new line. | [optional] 
**Gross** | **bool** | gross (Preisangabe - Netto/Brutto) | [optional] [default to false]
**PermissionType** | **long** | 0 &#x3D; Öffentlich, 1 &#x3D; Benutzerdefiniert, 2 &#x3D; Persönlich | [optional] 
**ReadPermissionProfiles** | **string** | comma separated list of profiles for the read permissions, evaluated only if permissionType equals 1. | [optional] 
**ReadPermissionLogins** | **string** | comma separated list of user login emails for the read permissions, evaluated only if permissionType equals 1. | [optional] 
**WritePermissionProfiles** | **string** | comma separated list of profiles for full access, evaluated only if permissionType equals 1. | [optional] 
**WritePermissionLogins** | **string** | comma separated list of user login emails, evaluated only if permissionType equals 1. | [optional] 
**CustomFields** | [**List&lt;CustomField&gt;**](CustomField.md) | Benutzerdefinierte Felder | [optional] 
**PositionsForm** | [**PositionsForm**](PositionsForm.md) |  | [optional] 
**DeliveryDate** | **long** | deliveryDate (Leistungsdatum). The deliveryDate as epoch timestamp in milliseconds. | [optional] 
**DeliveryDateTo** | **long** | deliveryDateTo (Leistungsdatum-bis). The deliveryDateTo as epoch timestamp in milliseconds. | [optional] 
**IsEndDiscountAbsolute** | **bool** | isEndDiscountAbsolute (Schlussrabatt), true &#x3D; absolute, false &#x3D; percent | [optional] [default to false]
**EndDiscount** | **decimal** | endDiscount(Schlussrabatt), if isEndDiscountAbsolute is set to true, then endDiscount should be an amount, if isEndDiscountAbsolute is set to false then a percentage is needed. | [optional] 
**PaymentTypeName** | **string** | paymentTypeName (Zahlungsart). If not provided for new invoice, the payment type from the personal account (connected to the customerContactId) is used. Sets also paymentType. | [optional] 
**IsPaid** | **bool** | isPaid (Auf exportiertem Beleg als Bezahlt ausweisen) | [optional] [default to false]
**DiscountDate1** | **long** | discountDate1 (Skontodatum 1). The discountDate1 as epoch timestamp in milliseconds. If it is set either discountPercent1 or discountAmount1 must be set. If one of the payment term properties is provided (discountDate1, discountDate2, dueDate, discountPercent1, discountPercent2, discountAmount1, discountAmount2) when creating an invoice, then the form is used. Otherwise the payment term from the personal account (connected to the customerContactId) is used. | [optional] 
**DiscountDate2** | **long** | discountDate2 (Skontodatum 2). The discountDate2 as epoch timestamp in milliseconds. It must be greater than discountDate1. If it is set either discountPercent2 or discountAmount2 must be set. | [optional] 
**DueDate** | **long** | dueDate (Fälligkeitsdatum). The dueDate as epoch timestamp in milliseconds. It must be greater than discountDate2. | [optional] 
**DiscountPercent1** | **decimal** | discountPercent1 (Skontosatz1). Either discount percent(s) or discount amount(s) are allowed. Setting both is not possible. If set discountDate1 must be set too. | [optional] 
**DiscountPercent2** | **decimal** | discountPercent2 (Skontosatz2). Either discount percent(s) or discount amount(s) are allowed. Setting both is not possible. If set discountDate2 must be set too. | [optional] 
**ExplicitDiscount** | **bool** | explicitDiscount (Skontobeträge manuell eingeben). If set discountAmount(s) must be provided | [optional] [default to false]
**DiscountAmount1** | **decimal** | discountAmount1 (Skontobetrag1). Only evaluated if explicitDiscount is true. If set discountDate1 must be set too. | [optional] 
**DiscountAmount2** | **decimal** | discountAmount2 (Skontobetrag2). Only evaluated if explicitDiscount is true. If set discountDate2 must be set too. | [optional] 
**OrderNumber** | **string** | orderNumber (Bestellnummer) | [optional] 
**ZoneNumber** | **string** | zoneNumber (Rechnungskreis). Please check your zone settings in the Scopevisio client application if the field is mandaotry. | [optional] 
**StateName** | **string** | state (Status des Auftrags). Sets also state. | [optional] 
**Locale** | **string** | locale (Sprache) | [optional] 

[[Back to Model list]](../README.md#documentation-for-models)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to README]](../README.md)

