
# CompuMaster.Scopevisio.OpenApi.Model.ImpersonalAccountRow

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Number** | **string** | impersonal account number | 
**Name** | **string** | impersonal account name | 
**Type** | **string** | The account type (Kontoart). | 
**Category** | **string** | The account category (Kontokennzeichen). | [optional] 
**VatCode** | **string** | vatCode (Steuerkennzeichen), the allowable values are the standard values for SKR03, see vat matrix in Scopevisio client if you have different settings in your chart of accounts. | [optional] 
**VatKey** | **string** | vatKey (Steuerschlüssel), the allowable values are the standard values for SKR03, see vat matrix in Scopevisio client if you have different settings in your chart of accounts. | [optional] 
**FixVatKey** | **bool** | is the vat key fixed or can another vat key be used when post to this account | [optional] [default to false]
**Deactivated** | **bool** | it is not possible to post to this account | [optional] [default to false]
**DirectPosting** | **bool** | is direct posting possible | [optional] [default to false]
**DiscountAccount** | **string** | discount account | [optional] 
**IsFinanceAccount** | **bool** | is finance account | [optional] [default to false]
**SubjectToClearing** | **bool** | is it possible to use the account for clearing | [optional] [default to false]
**Dimensions** | [**List&lt;DimensionForm&gt;**](DimensionForm.md) |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to README]](../README.md)

