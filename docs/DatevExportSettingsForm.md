
# CompuMaster.Scopevisio.OpenApi.Model.DatevExportSettingsForm

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**FromPostingDateTs** | **long** | fromPostingDateTs. Timestamp in milliseconds to set the posting date from. If not set the first day of the current year is set. | [optional] 
**ToPostingDateTs** | **long** | toPostingDateTs. Timestamp in milliseconds to set the posting date to. If not set the last day of the current year is set. | [optional] 
**FromCreationDateTs** | **long** | fromCreationDateTs. Timestamp in milliseconds to set the creation date from. Both fromCreationDateTs and toCreationDateTs must be provided if creation date filtering needed. | [optional] 
**ToCreationDateTs** | **long** | toCreationDateTs. Timestamp in milliseconds to set the creation date to. Both fromCreationDateTs and toCreationDateTs must be provided if creation date filtering needed. | [optional] 
**Dimension1** | **string** | dimension1. The name of the dimension which should be exported to Kost1 in DATEV. | [optional] 
**Dimension2** | **string** | dimension2. The name of the dimension which should be exported to Kost2 in DATEV. | [optional] 
**TextConfiguration** | **string** | textConfiguration. The name of the text field which should be exported to Text in DATEV. If not set posting text(Belegtext) is used. | [optional] 
**Field1Configuration** | **string** | field1Configuration. The name of the field which should be exported to Text in DATEV. If not set external number(Externe Belegnummer) is used. | [optional] 
**ExportStrategy** | **string** | exportStrategy. The way split entries are exported. If not set standard is used. | [optional] 
**SkipCancellations** | **bool** | skipCancellations. Do you want to export cancellation postings? | [optional] [default to false]
**VatMatrixDatevKeys** | **bool** | vatMatrixDatevKeys. Do you want to use the DATEV posting keys (BU-Schlüssel) from Scopevisios vat matrix? Standard value is true. | [optional] [default to false]

[[Back to Model list]](../README.md#documentation-for-models)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to README]](../README.md)

