
# CompuMaster.Scopevisio.OpenApi.Model.ContactPropertyForm

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Type** | **string** | Sets the values in column \&quot;Art der Information\&quot;. For allowable values see catalog Systemadministration-&gt;System -&gt; Auswahllisten Verwalten -&gt; Allgemeine Kontaktdaten -&gt; Art der Information | [optional] 
**Subtype** | **string** | Sets the values in column \&quot;Untergruppierung\&quot;. For allowable values see catalog Systemadministration-&gt;System -&gt; Auswahllisten Verwalten -&gt; Allgemeine Kontaktdaten -&gt; Untergruppierung | [optional] 
**Description** | **string** | Description | [optional] 
**ValidFrom** | **long** | validFrom | [optional] 
**ValidTo** | **long** | validTo | [optional] 
**EmployeeDomain** | **bool** | employeeDomain | [optional] [default to false]
**SpheresDisplay** | **string** | spheres. Not set means visible in all spheres.  | [optional] 
**EmployeeFilterDisplay** | **string** | In which employer contact should this information be visible. Not set equals \&quot;Bei allen AG\&quot; | [optional] 
**PermissionType** | **long** | 0 &#x3D; Öffentlich, 1 &#x3D; Benutzerdefiniert, 2 &#x3D; Persönlich | [optional] 
**ReadPermissionProfiles** | **string** | comma separated list of profiles for the read permissions, evaluated only if permissionType equals 1. | [optional] 
**ReadPermissionLogins** | **string** | comma separated list of user login emails for the read permissions, evaluated only if permissionType equals 1. | [optional] 
**WritePermissionProfiles** | **string** | comma separated list of profiles for full access, evaluated only if permissionType equals 1. | [optional] 
**WritePermissionLogins** | **string** | comma separated list of user login emails, evaluated only if permissionType equals 1. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to README]](../README.md)

