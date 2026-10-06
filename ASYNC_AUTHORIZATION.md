# Cancellable asynchronous authorization

This follow-up adds cancellation-token overloads to `AdditionalApi.GetApplicationContextAsyncWithHttpInfo` and `AuthorizationApi.TokenAsyncWithHttpInfo`. The previous signatures remain available and delegate with `CancellationToken.None`. Cancellation reaches RestSharp's native HTTP request and is checked before response processing, so an aborted request is reported as cancellation rather than a synthetic HTTP error.

`OpenScopeApiClient.AuthorizeWithUserCredentialsAsync` installs tokens using configured credentials. `RefreshAuthorizationAsync` uses the installed refresh token and rejects refresh without one. Both operations serialize asynchronous authorization on the client instance; waiting and active HTTP requests can be canceled. Existing synchronous entry points keep their previous contracts. Changing configuration or using the synchronous API concurrently still requires caller coordination.

The dedicated isolated project passes **three tests on net8.0 and three on net48**, using a custom fake HttpClient. Tests cover cancellation of active token and account-context requests, token installation, and refresh-token request construction. The library builds on `netstandard2.0`, `net6.0`, and `net48`.

```powershell
dotnet test src/CompuMaster.Scopevisio.OpenApi.AsyncTests/CompuMaster.Scopevisio.OpenApi.AsyncTests.csproj -c CI_CD
```

No live authentication or server-mutating integration test was run. Teamwork/DMS consumption and coordinated integration verification remain pending. This draft does not publish a package or authorize a release.
