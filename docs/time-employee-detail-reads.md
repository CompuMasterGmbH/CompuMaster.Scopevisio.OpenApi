# Selected time-entry and project-team details

ProjectApi.GetTimeEntryDetails and GetProjectEmployeeDetails provide complete JToken results through synchronous, HTTP-info and asynchronous forms, including native cancellation. They cover GET /timeentry/{id} and GET /project/{projectId}/projectemployee/{employeeId}. The team-entry overload retains the existing employeeId, projectId argument order. Its employeeId identifies a project-team entry rather than a contact; no contact lookup is performed. The profiles are Projekte (Anzeigen) for time entries and Kontakte, Projekte (Anzeigen) for team entries.

Selectors retain the complete signed Int64 range with invariant text; no minimum is declared by the manufacturer. Original optional fields are sent as supplied: null omits the query parameter and an empty string remains explicit. Requests have no body, formatting, pagination or inferred defaults. The contract provides no fixed successful response shape or exhaustive GET error-status list, so complete objects, arrays, scalars, future fields and explicit null remain available.

WithHttpInfo retains status, combined response/content headers and exact raw JSON. Date strings remain strings. Parsed fractions use .NET decimal precision and may round, underflow or exceed its range; raw JSON remains exact. The existing exception factory runs before parsing, original errors remain server-authoritative and cancellation reaches admission and active native HTTP I/O.

Historical GetTimeEntry and GetProjectEmployee methods retain their void/null-data behavior. Configuration identity, caller-owned transport, token/account defaults and existing mutation methods remain unchanged. The one-selector/no-query bounded generator rejects these operation shapes; these additions are separately reviewed handwritten partials.

[SDK #114](https://github.com/CompuMasterGmbH/CompuMaster.Scopevisio.OpenApi/issues/114) tracks both workflows, isolated .invalid protocol fixtures, compiled compatibility and remaining live acceptance. Actual selected records, profile/visibility and server diagnostics require a coordinated owned account window. Publication and downstream adoption remain separate.
