
# Org.OpenAPITools.Model.TravelEntryForm

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**EmployeeId** | **long** | Id of contact (Reisender) | 
**AccountType** | **string** | accountType(Beschaeftigungart). Default value is \&quot;Mitarbeiter\&quot; if not set by the user.  | [optional] 
**Documentnumber** | **string** | documentNumber(Reisenummer). \&quot;documentNumber\&quot; if not provided a document number is generated.  | [optional] 
**OrderId** | **long** | orderId(Auftrag) | [optional] 
**DimensionNumber** | **long** | dimensionNumber(Kostenstelle) | [optional] 
**ProjectId** | **long** | projectId(Projekt) | [optional] 
**Note** | **string** | note(Anlass der Reise). | [optional] 
**StartTs** | **long** | start timestamp(Reisebeginn).\&quot;startTs\&quot; is a mandatory field. If not given it is not work. | 
**EndTs** | **long** | end timestamp(Reiseende).\&quot;endTs\&quot; is a mandatory field. If not given it is not work. | 
**StartContactId** | **long** | contactId(kontakt) | [optional] 
**StartStreet** | **string** | startStreet and startNumber(straße und hausnummer) | [optional] 
**StartCity** | **string** | startCity(stadt) | [optional] 
**StartPostcode** | **string** | startPostcode(postleitzahl) | [optional] 
**StartCountry** | **string** | startCountry(land) | [optional] 
**StartFederalState** | **string** | startFederalState(bundesland) | [optional] 
**Street** | **string** | street and number(straße und Hausnummer) | [optional] 
**City** | **string** | city(stadt) | [optional] 
**Postcode** | **string** | postcode(postleitzahl) | [optional] 
**Country** | **string** | county(land) | [optional] 
**FederalState** | **string** | federalstate(bundesland) | [optional] 
**InternalNote** | **string** | internal Note(Interne Notiz) | [optional] 
**StateDone** | **bool** | stateDone(Durchgeführt) | [optional] [default to false]
**Billable** | **bool** | billable(Fakturierbar) | [optional] [default to false]
**PermissionType** | **long** | 0 &#x3D; Öffentlich, 1 &#x3D; Benutzerdefiniert, 2 &#x3D; Persönlich | [optional] 
**ReadPermissionProfiles** | **string** | comma separated list of profiles for the read permissions, evaluated only if permissionType equals 1. | [optional] 
**ReadPermissionLogins** | **string** | comma separated list of user login emails for the read permissions, evaluated only if permissionType equals 1. | [optional] 
**WritePermissionProfiles** | **string** | comma separated list of profiles for full access, evaluated only if permissionType equals 1. | [optional] 
**WritePermissionLogins** | **string** | comma separated list of user login emails, evaluated only if permissionType equals 1. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to README]](../README.md)

