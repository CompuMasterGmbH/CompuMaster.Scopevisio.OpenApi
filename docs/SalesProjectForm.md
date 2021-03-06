
# CompuMaster.Scopevisio.OpenApi.Model.SalesProjectForm

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Number** | **string** | number | [optional] 
**Name** | **string** | name | 
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
**LeadBudget** | **string** | Lead Bugdet.Please check your catalog in the Scopevisio desktop client for allowable values | [optional] 
**LeadBudgetSum** | **decimal** | Lead Bugdet Sum | [optional] 
**LeadAuthority** | **string** | Lead Authority. Please check your catalog in the Scopevisio desktop client for allowable values | [optional] 
**LeadNeed** | **string** | Lead Need. Please check your catalog in the Scopevisio desktop client for allowable values | [optional] 
**LeadTime** | **string** | Lead Time. Please check your catalog in the Scopevisio desktop client for allowable values | [optional] 
**SalesPersonId** | **long** | salesPersonId (Vertriebsbeauftragter) - this must be the id of a user contact (Aus Benutzerkontakt) | [optional] 
**LeadNotice** | **string** | leadNotice (Vertriebsinformationen) | [optional] 
**InterestedBudget** | **string** | Interested Bugdet.Please check your catalog in the Scopevisio desktop client for allowable values. Set only when the project has \&quot;sphereInterested\&quot; set to true. | [optional] 
**InterestedBudgetSum** | **decimal** | Interested Bugdet Sum. Set only when the project has \&quot;sphereInterested\&quot; set to true. Please check your catalog in the Scopevisio desktop client for allowable values. | [optional] 
**InterestedAuthority** | **string** | Interested Authority. Set only when the project has \&quot;sphereInterested\&quot; set to true. Please check your catalog in the Scopevisio desktop client for allowable values. | [optional] 
**InterestedNeed** | **string** | Interested Need. Set only when the project has \&quot;sphereInterested\&quot; set to true. Please check your catalog in the Scopevisio desktop client for allowable values. | [optional] 
**InterestedTime** | **string** | Interested Time. Set only when the project has \&quot;sphereInterested\&quot; set to true. Please check your catalog in the Scopevisio desktop client for allowable values. | [optional] 
**SalesKontaktType** | **string** | salesKontaktType (Vertriebsquelle, Leadquelle) | [optional] 
**SalesInternalOrganisation** | **string** | salesInternalOrganisation (Vertriebsorgansiation). Please check your catalog in the Scopevisio desktop client for allowable values. | [optional] 
**SalesContractPerson** | **long** | salesContractPerson (Entscheider). The contact must be an employee of the project contact (contactId). | [optional] 
**SalesChannel** | **string** | salesChannel(Vertriebsweg). Please check your catalog in the Scopevisio desktop client for allowable values. | [optional] 
**SalesCampaignName** | **string** | salesCampaignName (Vertriebskampagne) | [optional] 
**SalesActivity** | **string** | salesActivity (Vertriebliche Folgeaktivität). Please check your catalog in the Scopevisio desktop client for allowable values. | [optional] 
**LeadPersonId** | **long** | leadPersonId (Marketingbeauftragter).  This must be the id of a user contact (Aus Benutzerkontakt). | [optional] 
**ReminderDate** | **long** | reminderDate (Wiedervorlage). | [optional] 
**SalesPhase** | **string** | salesPhase (Vertriebsphase). Please check your catalog in the Scopevisio desktop client for allowable values. | [optional] 
**SalesStatus** | **string** | salesStatus (Vertriebsstatus). Please check your catalog in the Scopevisio desktop client for allowable values. | [optional] 
**SalesPriority** | **string** | salesPriority (Vertriebspriorität). Please check your catalog in the Scopevisio desktop client for allowable values. | [optional] 
**SalesRisk** | **string** | salesRisk (Vertriebsrisiko). Please check your catalog in the Scopevisio desktop client for allowable values. | [optional] 
**SalesReminderFrom** | **long** | salesReminderFrom (Wiedervorlagen nächste). | [optional] 
**SalesReminderTo** | **long** | salesReminderTo (Wiedervorlagen weitere). | [optional] 
**SalesDescription** | **string** | salesDescription (Vertriebsprojektbeschreibung). | [optional] 
**ReplacementProductName** | **string** | replacementProductName (Abzulösendes Bestandsprodukt). Please check your catalog in the Scopevisio desktop client for allowable values. | [optional] 
**ResidualProductName** | **string** | residualProductName (Verbleibende Bestandsprodukt). Please check your catalog in the Scopevisio desktop client for allowable values. To set more than one poduct use \&quot;¤¤¤\&quot; as a separator. | [optional] 
**CompetitionProductsName** | **string** | competitionProductsName (Wettbewerbsprodukte). Please check your catalog in the Scopevisio desktop client for allowable values. To set more than one poduct use \&quot;¤¤¤\&quot; as a separator. | [optional] 
**ContractObligation** | **long** | contractObligation (Vetragsbindung). | [optional] 
**SalesPotenzial** | **string** | salesPotenzial (Vertriebsinfo). | [optional] 
**SalesUpgrade** | **bool** | salesUpgrade (Upgrade). | [optional] [default to false]
**ServicePartnerName** | **string** | servicePartnerName (Betreuender Servicepartner). Please check your catalog in the Scopevisio desktop client for allowable values. | [optional] 
**CompetitiveStrength** | **string** | competitiveStrength (Wettbewerbsstärke). Please check your catalog in the Scopevisio desktop client for allowable values. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to README]](../README.md)

