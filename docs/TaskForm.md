
# Org.OpenAPITools.Model.TaskForm

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Topic** | **string** | topic | 
**Priority** | **string** | priority | [optional] 
**ContactId** | **long** | contactId | [optional] 
**ContactLegacyId** | **string** | contactLegacyId, if a contact with the given contactId is found, contactLegacyId is not evaluated | [optional] 
**ContactSphere** | **string** | contactSphere | [optional] 
**Description** | **string** | description | [optional] 
**StartDateTS** | **long** | startDateTS | [optional] 
**DueDateTS** | **long** | dueDateTS | [optional] 
**ResponsibleContactId** | **long** | responsibleContactId | [optional] 
**ReminderDateTS** | **long** | reminderDateTS | [optional] 
**Status** | **string** | status | [optional] 
**ProjectId** | **long** | projectId | [optional] 
**OutgoingInvoiceId** | **long** | outgoingInvoiceId | [optional] 
**Tags** | **string** | tags | [optional] 
**EmployeeDomain** | **bool** | creates an employee task | [optional] [default to false]
**SetEstimatedHoursManually** | **bool** | allows setting estimated hours manually | [optional] [default to false]
**EstimatedHours** | **decimal** | this parameter is evaluated only if setEstimatedHoursManually is set to true | [optional] 
**CustomFields** | [**List&lt;CustomField&gt;**](CustomField.md) |  | [optional] 
**PermissionType** | **long** | 0 &#x3D; Öffentlich, 1 &#x3D; Benutzerdefiniert, 2 &#x3D; Persönlich | [optional] 
**ReadPermissionProfiles** | **string** | comma separated list of profiles for the read permissions, evaluated only if permissionType equals 1. | [optional] 
**ReadPermissionLogins** | **string** | comma separated list of user login emails for the read permissions, evaluated only if permissionType equals 1. | [optional] 
**WritePermissionProfiles** | **string** | comma separated list of profiles for full access, evaluated only if permissionType equals 1. | [optional] 
**WritePermissionLogins** | **string** | comma separated list of user login emails, evaluated only if permissionType equals 1. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to README]](../README.md)

