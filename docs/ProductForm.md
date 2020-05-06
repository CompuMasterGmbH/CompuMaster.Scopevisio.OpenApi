
# CompuMaster.Scopevisio.OpenApi.Model.ProductForm

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Number** | **string** | number(Produktnummer). \&quot;number\&quot; is a mandatory field when saving a product. If a new product is created and no \&quot;number\&quot; provided a value is generated and assigned to the product automatically. | [optional] 
**Name** | **string** | name(Bezeichnung) | [optional] 
**Description** | **string** | description(Beschreibung) | [optional] 
**Type** | **long** | type(Produkttyp nummerisch) \&quot;type\&quot; is set automtically on the basis of \&quot;typeDisplay\&quot;.  | [optional] [readonly] 
**TypeDisplay** | **string** | typeDisplay(Produktart), default value is \&quot;Ware\&quot; if not set by the user. | [optional] 
**InvoicePostingMode** | **string** | invoicePostingMode(Zuordnung Rechnungsart), default value is \&quot;Einzel-/Teil-/Schlussrechnung\&quot; if not set by the user   | [optional] 
**TaxRate** | **decimal** | taxRate(Steuersatz), default value is 0% if not set by the user. | [optional] 
**RevenueAccountMode** | **long** | revenueAccountMode(0 &#x3D; Standard-Erlöskonten verwenden, 1 &#x3D; Manuelle Erlöskonten verwenden). If revenue accounts are provided (\&quot;revenueAccount0\&quot;, etc.), it is set automatically to 1. Default value is 0 Standard-Erlöskonten verwenden) if not set by the user explicitly.  | [optional] 
**RevenueAccount0** | **string** | revenueAccount0(Manuelle erlöskonten verwenden - Inland) | [optional] 
**RevenueAccount1** | **string** | revenueAccount1(Manuelle erlöskonten verwenden - EU Ausland) | [optional] 
**RevenueAccount2** | **string** | revenueAccount2(Manuelle erlöskonten verwenden - Ausland) | [optional] 
**RevenueAccount3** | **string** | revenueAccount3(Manuelle erlöskonten verwenden - Abschlagsrechnungen) | [optional] 
**Unit** | **string** | Unit(Einheit). For more untits please check the Scopevisio client application, catalog Abrechnungsbelege -&gt; Einheit. | 
**AsIfSold** | **bool** | asIfSold(As-if-sold) | [optional] [default to false]
**Billable** | **bool** | billable(Fakturierbar) | [optional] [default to false]
**Locked** | **bool** | locked(Gesperrt) | [optional] [default to false]
**PlanValue** | **decimal** | planValue(Planwert) | [optional] 
**CostPrice** | **decimal** | costPrice(Einkaufspreis) | [optional] 
**SingleAmount** | **decimal** | singleAmount(Netto Preisgruppe 1) | [optional] 
**SingleAmount2** | **decimal** | singleAmount2(Netto Preisgruppe 2) | [optional] 
**SingleAmount3** | **decimal** | singleAmount3(Netto Preisgruppe 3) | [optional] 
**SingleAmountGross** | **decimal** | singleAmountGross(Brutto Preisgruppe 1) | [optional] 
**SingleAmountGross2** | **decimal** | singleAmountGross2(Brutto Preisgruppe 2) | [optional] 
**SingleAmountGross3** | **decimal** | singleAmountGross3(Brutto Preisgruppe 3) | [optional] 
**NameLng1** | **string** | nameLng1(Englisch Bezeichnung) | [optional] 
**DescriptionLng1** | **string** | descriptionLng1(Englisch Beschreibung) | [optional] 
**ProductGroupName** | **string** | productGroupName(Produktgruppen) | [optional] 

[[Back to Model list]](../README.md#documentation-for-models)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to README]](../README.md)

