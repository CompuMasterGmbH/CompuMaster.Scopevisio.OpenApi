
# Org.OpenAPITools.Model.OutgoingInvoicePositionForm

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**TypeDisplay** | **string** | typeDisplay (Typ). Sets also type (Typ-Id). If invalid value provided or not provided at all, the typeDisplay is set to \&quot;Produkt\&quot;. | [optional] 
**Number** | **string** | number (Produktnummer) | [optional] 
**Name** | **string** | name (Bezeichnung). Mandatory for positionsForm of types \&quot;Überschrift\&quot; and \&quot;Freitext\&quot; | [optional] 
**Description** | **string** | description (Beschreibung) | [optional] 
**Quantity** | **decimal** | quantity (Menge) | [optional] 
**Unit** | **string** | unit (Einheit) | [optional] 
**SingleAmount** | **decimal** |  singleAmount (Eizelpreis). If the invoice is nett (Netto) this will be the net amount, if the invoice is gross, this will be the gross amount for the position. After calculation the following fields are set: singleAmount(Einzelpreis (Netto)), singleAmountGross(Einzelpreis (Brutto)), amount(Gesamtpreis(Netto)), amountGross(Gesamtpreis (Brutto)) | [optional] 
**Account** | **string** | account (Konto) | [optional] 
**VatKey** | **string** | vatKey(Steuerschlüssel) | [optional] 
**Discount** | **decimal** | discount (Rabatt (%)) | [optional] 
**FreeText1** | **string** | freeText1 (Freitext 1) | [optional] 
**FreeText2** | **string** | freeText2 (Freitext 2) | [optional] 
**SingleCostAmount** | **decimal** | singeCostAmount (Einkaufs-Einzelpreis(netto)) | [optional] 
**ValutaDate** | **long** | valutaDate (Umsatzzeitpunkt) | [optional] 
**AsIfSold** | **bool** | asIfSold (As-if-sold) Only used in opportunity and offer | [optional] [default to false]

[[Back to Model list]](../README.md#documentation-for-models)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to README]](../README.md)

