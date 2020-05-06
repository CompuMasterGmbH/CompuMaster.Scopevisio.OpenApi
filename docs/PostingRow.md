
# CompuMaster.Scopevisio.OpenApi.Model.PostingRow

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**PostingDate** | **long** | posting date | 
**DocumentDate** | **long** | document date | [optional] 
**DocumentNumber** | **string** | document number | 
**ExternalDocumentNumber** | **string** | external document number | [optional] 
**Account** | **string** | account (personal or impersonal account) | 
**SummaryAccount** | **string** | summary account (required if a personal account is used) | [optional] 
**Amount** | **decimal** | amount | 
**CancelDocument** | **string** | number of documet for cancellation | [optional] 
**ForeignCurrencyAmount** | **decimal** | foreign currency amount | [optional] 
**ForeignCurrencyCode** | **string** | foreign currency code | [optional] 
**ForeignCurrencyRate** | **decimal** | foreign currency rate | [optional] 
**DocumentText** | **string** | document text | [optional] 
**RowText** | **string** | posting row text | [optional] 
**VatKey** | **string** | vat key | [optional] 
**Dimensions** | [**List&lt;DimensionForm&gt;**](DimensionForm.md) |  | [optional] 
**DiscountPercent1** | **decimal** | discount percent 1, evaluated if \&quot;account\&quot; is personal account | [optional] 
**DiscountPeriod1** | **long** | discount period 1, evaluated if \&quot;account\&quot; is personal account | [optional] 
**DiscountPercent2** | **decimal** | discount percent 2, evaluated if \&quot;account\&quot; is personal account | [optional] 
**DiscountPeriod2** | **long** | discount period 2, evaluated if \&quot;account\&quot; is personal account | [optional] 
**NettimeLimit** | **long** | net time limit, evaluated if \&quot;account\&quot; is personal account | [optional] 
**DisountAccount** | **string** | disountAccount(Skontokonto), evaluated if \&quot;account\&quot; is personal account | [optional] 
**PaymentType** | **string** | paymentType(Zahlungsart), evaluated if \&quot;account\&quot; is personal account | [optional] 
**InternalDocumentNumber** | **string** | internal document number | [optional] 

[[Back to Model list]](../README.md#documentation-for-models)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to README]](../README.md)

