# Selected bank connections and journal rows

AccountingApi.GetBankConnectionsForDebitorDetails, GetBankConnectionsForKreditorDetails and GetJournalEntryByDocumentNumberDetails return complete JToken results through synchronous, HTTP-info and native asynchronous forms. They cover debtor/creditor bank-connection GETs and journal posting rows selected by document number. Historical void/Object/null-data getters and existing fiscal mutations remain unchanged.

Original account-number and posting document-number strings are escaped without trimming, numeric conversion, leading-zero removal, bank/IBAN/date normalization or account/permission lookup. Blank and relative-dot selectors are rejected. Optional original fields retain null omission versus explicit empty text; bodyless requests add no other formatting/paging/default choices. The profiles are Stammdaten, Debitorenstamm (Anzeigen), Stammdaten, Kreditorenstamm (Anzeigen), and Journal (Anzeigen), respectively.

The manufacturer declares no fixed successful JSON shapes or exhaustive GET error-status lists. Complete objects, arrays, scalars, future fields and explicit null remain available; empty content returns null. WithHttpInfo retains status, combined response/content headers and exact raw JSON. Date strings remain strings; parsed fractions follow .NET decimal precision with possible rounding/underflow/overflow, while raw JSON stays exact.

Original server diagnostics and the caller's exception factory remain authoritative; the factory runs before parsing. Native cancellation reaches admission and active HTTP I/O without speculative replay. Configuration, OAuth/token/account selection and default behaviour remain intact. No bank connection, journal entry, payment or fiscal state is modified.

[SDK #126](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/126) tracks three current contracts, fake .invalid selector/result/field/error/cancellation fixtures, compiled compatibility and remaining coordinated real account/journal/profile/projection acceptance. Publication and downstream adoption stay separate.
