
# CompuMaster.Scopevisio.OpenApi.Model.ProjectForm

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Number** | **string** | number | [optional] 
**Name** | **string** | name | 
**Category** | **string** | category (Projektkategorie), if not given, it is set to \&quot;Kundenprojekt\&quot; | [optional] 
**Type** | **string** | type (Projektbezug)  | [optional] 
**Type2** | **string** | type2 (Projektart) - the allowable values depend on the project category. Please check the Scopevisio desktop application for all allowable values per category. The example values are for category \&quot;Kundenprojekt\&quot; | [optional] 
**Description** | **string** | description (Projektbeschreibung) | [optional] 
**ContactId** | **long** | contactId (Projektadressat) | 
**LegacyNumber** | **string** | legacyNumber (ID Vorsystem). The Id of the project used in the previous system. | [optional] 
**InitiatorContactId** | **long** | project initiator (Projektträger) | [optional] 
**ContactPerson** | **long** | contactPerson (Ansprechpartner beim Kunden/Lead/Interessenten). The contact id of the contact person for the project - the selected contact must be an employee of the project contact and the project contact must be main employer. | [optional] 
**OwnerUid** | **string** | ownerUid (Projektinhaber). Uid of the user, who is the owner of this project. Refer to /users to retrieve the uid | [optional] 
**Workdays** | **decimal** | workdays (Arbeitstage). Please check the Scopevisio desktop application for all allowable values for workdays | [optional] 
**StartTs** | **long** | Project start (Projektstart) | [optional] 
**EndTs** | **long** | project end (Projektende) | [optional] 
**DecisionTs** | **long** | Project decision timestamp (Entsch.zeitp.) | [optional] 
**EstimatedHoursMode** | **bool** | estimatedHoursMode (checbox Manuell). Toggle if estimated hours is set manually or not, 0 &#x3D; false, 1 &#x3D; true | [optional] [default to false]
**EstimatedHours** | **decimal** | Estimated hours field is evaluated only if estimatedHoursMode is present and set to true | [optional] 
**DimensionNumber** | **long** | dimensionNumber (Projektkostenstelle) | [optional] 
**DimensionName** | **string** | dimensionName (Projektkostenstelle) Name of the dimensionNumber. If not given, it is set to the project name. | [optional] 
**Phase** | **string** | phase (Projekt-Phase) - the allowable values depend on the project category. Please check the Scopevisio desktop application for all allowable values per category. The example values are for category \&quot;Kundenprojekt\&quot; | [optional] 
**Status** | **string** | status (Projekt-Status) - the allowable values depend on the project category. Please checkthe Scopevisio desktop application for all allowable values per category. The example values are for category \&quot;Kundenprojekt\&quot; | [optional] 
**Priority** | **string** | priority (Projekt-Priorität) | [optional] 
**Risk** | **string** | risk (Projekt-Risiko) | [optional] 
**RiskDescription** | **string** | riskDescription (Risikobeschreibung) | [optional] 
**Billable** | **string** | billable (Fakturierbar) | [optional] 
**TimeRecordable** | **string** | timeRecordable (Zeiterfassung) | [optional] 
**TimeRecordType** | **string** | time record type (Zeiterfassung in...) | [optional] 
**HoursPerDay** | **long** | hours per day (Stunden pro Arbeitstag) | [optional] 
**Mail** | **string** | project email (E-Mail) | [optional] 
**CustomFields** | [**List&lt;CustomField&gt;**](CustomField.md) | custom fields (Benutzerdefinierte Felder)- these are available for projects only if your client supports sales projects | [optional] 
**ProjectEmployees** | [**List&lt;ProjectEmployeeForm&gt;**](ProjectEmployeeForm.md) | project employees are all contacts involved in the project (Projektbeteiligte) | [optional] 
**PermissionType** | **long** | permissionType(Berechtigungen)0 &#x3D; Öffentlich, 1 &#x3D; Benutzerdefiniert, 2 &#x3D; Persönlich | [optional] 
**ReadPermissionProfiles** | **string** | comma separated list of profiles for the read permissions, evaluated only if permissionType equals 1. | [optional] 
**ReadPermissionLogins** | **string** | comma separated list of user login emails for the read permissions, evaluated only if permissionType equals 1. | [optional] 
**WritePermissionProfiles** | **string** | comma separated list of profiles for full access, evaluated only if permissionType equals 1. | [optional] 
**WritePermissionLogins** | **string** | comma separated list of user login emails, evaluated only if permissionType equals 1. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to README]](../README.md)

