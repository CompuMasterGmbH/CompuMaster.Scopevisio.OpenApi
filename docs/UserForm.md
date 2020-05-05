
# Org.OpenAPITools.Model.UserForm

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Uid** | **string** | User ID | [optional] 
**FirstName** | **string** | First name | 
**LastName** | **string** | Last name | 
**Gender** | **string** | Gender | [optional] 
**Email** | **string** | Email address used as username | 
**ShortName** | **string** | User signature | [optional] 
**Administrator** | **bool** | Indicates whether this user is an administrator | [optional] [default to false]
**Password** | **string** | User password. Must be at least 6 characters long | [optional] 
**ChangePassword** | **bool** | Indicates whether the user has to change the password | [optional] [default to false]
**Profiles** | [**List&lt;Profile&gt;**](Profile.md) | User profiles to assign | [optional] 
**Licenses** | [**List&lt;License&gt;**](License.md) | Product licenses to assign | [optional] 
**BuyMissingLicenses** | **bool** | Wether to buy licenses, if not available | [optional] [default to false]
**Status** | **string** | User status | [optional] [readonly] 
**SpecialRights** | **List&lt;string&gt;** | User&#39;s special rights | [optional] [readonly] 
**CreationTs** | **long** | Creation timestamp | [optional] [readonly] 
**TeamworkRoles** | [**List&lt;TeamworkRole&gt;**](TeamworkRole.md) | Users role rights for teamwork. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to README]](../README.md)

