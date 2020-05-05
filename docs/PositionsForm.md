
# Org.OpenAPITools.Model.PositionsForm

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**CopyProductsToPositions** | **bool** | Set to true if you want to use the existing products to create the positions. The product values are used only if the user has not provided values himself. E.g. if the singleAmount has been provided in the request body, the singleAmount from product is not used. | [optional] [default to false]
**Positions** | [**List&lt;OutgoingInvoicePositionForm&gt;**](OutgoingInvoicePositionForm.md) | positionsForm. Positions for outgoing invoice | 

[[Back to Model list]](../README.md#documentation-for-models)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to README]](../README.md)

