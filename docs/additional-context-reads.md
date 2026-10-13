# Complete Additional context and explicit token reads

Issue #140 contributes to #17 and consuming CompuMasterGmbH/CompuMaster.Dms#90. Four additive JSON workflows retain all historical typed methods, results, defaults and authentication/account behavior.

| Workflow | Current GET | Query |
| --- | --- | --- |
| GetHelloDetails | /hello | None |
| GetVersionDetails | /version | None |
| GetOrganisationsDetails | /organisations | None |
| GetTeamworkTokenDetails | /teamworktoken | Optional force |

All 24 synchronous/HTTP-info/asynchronous/native-cancellation forms send bodyless requests. Information/organisation reads have no query options. They do not choose an account/organisation/instance, sort or page inventory, infer permissions or acquire credentials automatically. These routes declare no required profiles; OAuth authentication still applies, as described in the [manufacturer's general documentation](https://help.scopevisio.com/de/articles/467359-general-documentation). Existing native myaccount/account-context support is already present and is not declared missing.

The token route independently preserves force omission, false and true. An explicit true asks the server to retrieve a new token instead of reusing an existing one; the SDK inserts no force/default and does not automatically refresh or install returned Teamwork credentials as the current API token. Data and raw responses may contain credentials and should remain confidential. HTTP 400 can mean unavailable Teamwork/refresh access, while HTTP 500 can mean refresh/access-token retrieval failure; original statuses, diagnostics and hooks survive. Actual token retrieval can affect server state and requires coordinated account acceptance separately from isolated synthetic responses.

Success schemas are undefined. Complete JToken results retain arbitrary arrays/envelopes, unknown fields, JSON null and date strings. HTTP-info retains exact raw JSON/status and combined response/content headers. Parsed fractional values use .NET decimal precision and may round, underflow or overflow; raw JSON remains exact. No account/organisation/token field widths, dates, role associations, model defaults or instance selection are inferred from incomplete success declarations.

The original configured factory inspects the response before parsing and preserves its exception instance. Native cancellation reaches active HTTP I/O. Existing Hello/Version/Records<Organisation>/TeamworkToken results remain unchanged. No token revocation, new authorization grant, account selection or retry is added.

Twelve absent HTTP-info cases reproduce before implementation. The 115 isolated cases cover all 24 forms, bodyless/query-free information reads, complete unknown JSON/raw/headers/status, hooks/errors and pre/active cancellation. All token forms separately cover omitted/false/true force and verify no API-token installation; old synchronous typed information/organisation/token identities retain their defaults. Only synthetic .invalid handlers are used; no actual credentials/tokens/accounts are accessed. Individual review/merge, current server/account acceptance, publication and downstream adoption remain separate.
