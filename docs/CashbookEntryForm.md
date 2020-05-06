
# CompuMaster.Scopevisio.OpenApi.Model.CashbookEntryForm

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**CashbookId** | **long** | cashbookId (Ref-Nummer Kasse) | 
**DocumentDate** | **long** | documentDate(Datum), epoch timestamp in milliseconds | 
**ExternalDocumentNumber** | **string** | externalDocumentNumber(externe Belegnummer) | [optional] 
**InternalDocumentNumber** | **string** | internalDocumentNumber (interne Belegnummer), | [optional] [readonly] 
**FreeText** | **string** | freeText(Belegtext) | [optional] 
**DebitAmount** | **decimal** | debitAmount(Einnahmen) | [optional] 
**CreditAmount** | **decimal** | creditAmount(Ausgaben) | [optional] 
**ImpersonalAccountNumber** | **string** | impersonalAccountNumber(Gegenkonto) | [optional] 
**PersonalAccountNumber** | **string** | Kunde/Lieferant | [optional] 
**Notice** | **string** | notice(Notiz) | [optional] 
**Dimensions** | [**List&lt;DimensionEntryValueForm&gt;**](DimensionEntryValueForm.md) |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to README]](../README.md)

