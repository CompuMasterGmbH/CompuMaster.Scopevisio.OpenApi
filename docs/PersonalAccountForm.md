
# CompuMaster.Scopevisio.OpenApi.Model.PersonalAccountForm

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**ContactId** | **long** | contactId (Kontakt Id), the id(Master Id) of the contact, to create a personal account for. Either valid contactId or customerNumber or email must be provided to create a personal account. | [optional] 
**CustomerNumber** | **string** | customerNumber, the customerNumber(Kontaktnummer) of the contact, to create a personal account for. Either valid contactId or customerNumber or email must be provided to create a personal account. | [optional] 
**Email** | **string** | email, the email(one of the existing seven possibilities E-Mail-Adresse[geschäftlich], E-Mail-Adresse[geschäftlich 2]. etc.) of the contact, to create a personal account for. Either valid contactId or customerNumber or email must be provided to create a personal account. | [optional] 
**PersonalAccountNumber** | **string** | personalAccountNumber (Kontonummer), the wished number of the created personal account. If no numberRangeNumber provided, then the number range fitting for the personalAccountNumber is set. | [optional] 
**NumberRangeNumber** | **long** | numberRangeNumber (Kreditoren-/Debitoren-Nummernkreis), the number of the number range for the personal account. If personalAccountNumber is not provided, the first available number for the provided numberRangeNumber is generated for the personal account.   If numberRangeNumber not provided,  the first found number range will be used. We recommend setting \&quot;numberRangeNumber\&quot; if more than one number ranges for this account type(debitor or kreditor) exist otherwise errors may occur if the number range is full.  The default number ranges in the Scopevisio desktop applications are \&quot;2\&quot; for debitors and \&quot;3\&quot; for creditors | [optional] 
**Group** | **string** | group (Kundengruppe/Kreditorengruppe), if the given group does not exist, it is created and assigned to the personal account. | [optional] 
**VatCode** | **string** | vatCode(Steuerkennzeichen) for the personal account. Please see vat matrix (Steuermatrix) in the Scopevisio client if you wish other than the standard allowable values. | [optional] 
**PaymentType** | **string** | paymentType(Zahlungsart) for the pesonal account. | [optional] 
**VatNumber** | **string** | vatNumber(Steuernummer) | [optional] 
**VatId** | **string** | vatId (Umsatzsteuer-ID) | [optional] 
**Currency** | **string** | currency(Währung), the ISO 4217 alphabetic currency code | [optional] 
**Language** | **string** | language(Sprache), please check the Scopeviso client application for allowable values | [optional] 
**PaymentTermId** | **long** | paymentTermId (Zahlungsbedingung id), the id of the wished payment term for the personal account. If an existing paymentTerm should be assigned to the personal account, then use \&quot;paymentTermId\&quot; . If a new paymentTerm should be created, use the parameter \&quot;paymentTermForm\&quot;. If both \&quot;paymentTermId\&quot; and \&quot;paymentTermForm\&quot; are provided, a payment term with the given \&quot;paymentTermId\&quot; is searched. If not found,the \&quot;paymentTermForm\&quot; is used. If a payment term with the given name is found, then it is assigned to the personal account, a new payment term is created otherwise. | [optional] 
**PaymentTermForm** | [**PaymentTermForm**](PaymentTermForm.md) |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to README]](../README.md)

