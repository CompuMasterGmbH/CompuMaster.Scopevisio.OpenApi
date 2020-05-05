
# Org.OpenAPITools.Model.TimeEntryForm

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**EmployeeId** | **long** | Id of the user contact (Aus Benutzerkontakt) | 
**StartTs** | **long** | Start timestamp. If not given it is set to the current time. | [optional] 
**EndTs** | **long** | End timestamp. If not given it is set to the current time. | [optional] 
**FullDay** | **bool** | fullDay. When set the data given in startTs and endTs is ignored. Both timestamps are set to the full day start and end time configured in the Scopevisio desktop application. | [optional] [default to false]
**ContactId** | **long** | contactId. If no contact is set explicitly by the user, the contact from project, task or order (in this priority order) is set. E.g. the user sets no contact and no project, but task and order. In this case the task contact is set to the time entry. If there is no contact in task, the order contact is set to the time entry. | [optional] 
**ProjectId** | **long** | projectId. If no project is set by the user explicitly the project from task or order(in this priority order) is set. E.g. the user sets no contact and no project, but task and order(in the order there is a project set). In this case the task project contact is set to the time entry. If there is no project in task, the order project is set to the time entry. | [optional] 
**TaskId** | **long** | taskId | [optional] 
**OrderId** | **long** | orderId | [optional] 
**ProductIdOrNumber** | **string** | productId or product number | [optional] 
**Billable** | **bool** | billable | [optional] [default to false]
**Confirmed** | **bool** | confirmed | [optional] [default to false]
**StateDone** | **bool** | done (Durchgeführt) | [optional] [default to false]
**InternalNote** | **string** | internal Note | [optional] 
**Note** | **string** | note | [optional] 
**PermissionType** | **long** | 0 &#x3D; Öffentlich, 1 &#x3D; Benutzerdefiniert, 2 &#x3D; Persönlich | [optional] 
**ReadPermissionProfiles** | **string** | comma separated list of profiles for the read permissions, evaluated only if permissionType equals 1. | [optional] 
**ReadPermissionLogins** | **string** | comma separated list of user login emails for the read permissions, evaluated only if permissionType equals 1. | [optional] 
**WritePermissionProfiles** | **string** | comma separated list of profiles for full access, evaluated only if permissionType equals 1. | [optional] 
**WritePermissionLogins** | **string** | comma separated list of user login emails, evaluated only if permissionType equals 1. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to README]](../README.md)

