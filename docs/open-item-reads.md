# Open-item and personal-journal reads

AccountingApi offers six synchronous/native asynchronous/HTTP-info/cancellation forms for each workflow:

| Method | POST route | Optional query |
|---|---|---|
| SearchCreditorOpenItems | /openitems/creditors | historicTs, accountNumber |
| SearchDebtorOpenItems | /openitems/debtors | historicTs, accountNumber |
| SearchCreditorPersonalJournalEntries | /openitems/creditor/list | None |
| SearchDebtorPersonalJournalEntries | /openitems/debitor/list | None |

These POST operations only retrieve records. They do not clear, rebook or change reminders or financial state. Creditor open items include outstanding incoming invoices/credit notes and unmatched payments; debtor open items include outgoing invoices/credit notes and unmatched payments. Journal searches return the server-selected personal-journal entries. Creditor/debitor/journal profiles and visibility remain server-enforced.

The optional SearchRequest sends one structured JSON object. The manufacturer declares a string schema but links its structured-object search documentation; the discrepancy is retained explicitly. Null omits the body, while an empty SearchRequest sends {}. Search paging, fields, count, ordering, formatting and unknown choices remain caller-controlled. JSON is serialized before HTTP I/O, so later caller mutations do not change an in-flight request.

For open-item searches, historicTs is nullable Int64 epoch milliseconds with invariant formatting and no DateTime/timezone conversion. Null omits it, while zero and other explicit values remain selected for server validation. accountNumber preserves original strings, including leading zeros/Unicode/whitespace; null omission and explicit empty text remain distinct. The client does not infer available historical periods or normalize account numbers.

Success responses have no fixed contract schema. JToken and HTTP-info preserve complete JSON and exact raw JSON/status/headers, without fixed row/amount assumptions, automatic paging, total-balance or completeness claims. Date-like strings and integer timestamps remain unchanged. Parsed fractions use bounded .NET decimal precision and may round or underflow; raw JSON remains exact. Empty bodies return null. The existing exception factory runs before parsing, preserving original diagnostics and method names; native cancellation reaches HTTP I/O.

Fake-handler fixtures use an .invalid origin and no credentials, GUI or real server. They cover all routes/method forms, copied structured search, null versus empty body, historic bounds/invariant query, exact account text, unknown reply shapes, errors/custom hooks and pre/active cancellation. Real financial profile/history/filter acceptance is separately coordinated under SDK #62; route exposure is not full or live support.
