using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CompuMaster.Scopevisio.OpenApi.Client;
using CompuMaster.Scopevisio.OpenApi.Model;
using Newtonsoft.Json.Linq;

namespace CompuMaster.Scopevisio.OpenApi
{
    public partial class OpenScopeApiClient
    {
        private Configuration CreateOrganisationAuthorizationConfiguration(Configuration source, long organisationId)
        {
            EnsureOrganisationSelection(source, organisationId);
            return new Configuration(new Dictionary<string, string>(source.DefaultHeader),
                new Dictionary<string, string>(source.ApiKey), new Dictionary<string, string>(source.ApiKeyPrefix), source.BasePath)
            {
                HttpClient = source.HttpClient, Timeout = source.Timeout, UserAgent = source.UserAgent,
                DateTimeFormat = source.DateTimeFormat, ClientNumber = source.ClientNumber,
                Username = source.Username, Password = source.Password, OrganisationId = organisationId
            };
        }

        private void EnsureOrganisationSelection(Configuration source, long organisationId)
        {
            if (!ReferenceEquals(Config, source) || source.OrganisationId != organisationId)
                throw new InvalidOperationException("The explicit organisation selection changed during authorization.");
        }

        private void AuthorizeOrganisation(Configuration source, long organisationId)
        {
            var staged = CreateOrganisationAuthorizationConfiguration(source, organisationId);
            using (staged.ApiClient.RestClient)
            {
                var authorization = new Api.AuthorizationApi(staged) { ExceptionFactory = AuthorizationApi.ExceptionFactory };
                var tokenResponse = authorization.TokenWithHttpInfo("password", staged.ClientNumber,
                    username: staged.Username, organisationId: organisationId, password: staged.Password);
                ValidateOrganisationToken(tokenResponse, organisationId);
                var additional = new Api.AdditionalApi(staged) { ExceptionFactory = AdditionalApi.ExceptionFactory };
                var accountResponse = additional.GetApplicationContextWithHttpInfo();
                ValidateOrganisationAccount(accountResponse, tokenResponse.Data, organisationId);
                InstallOrganisationToken(source, organisationId, tokenResponse.Data);
            }
        }

        private async Task AuthorizeOrganisationAsync(Configuration source, long organisationId, string refreshToken, string expectedUserId,
            CancellationToken cancellationToken)
        {
            var staged = CreateOrganisationAuthorizationConfiguration(source, organisationId);
            using (staged.ApiClient.RestClient)
            {
                var authorization = new Api.AuthorizationApi(staged) { ExceptionFactory = AuthorizationApi.ExceptionFactory };
                var tokenResponse = refreshToken == null
                    ? await authorization.TokenAsyncWithHttpInfo("password", staged.ClientNumber, cancellationToken,
                        username: staged.Username, organisationId: organisationId, password: staged.Password).ConfigureAwait(false)
                    : await authorization.TokenAsyncWithHttpInfo("refresh_token", staged.ClientNumber, cancellationToken,
                        refreshToken: refreshToken).ConfigureAwait(false);
                ValidateOrganisationToken(tokenResponse, organisationId);
                if (expectedUserId != null && !string.Equals(tokenResponse.Data.Uid, expectedUserId, StringComparison.Ordinal))
                    throw new InvalidDataException("The refreshed token does not confirm the installed user.");
                var additional = new Api.AdditionalApi(staged) { ExceptionFactory = AdditionalApi.ExceptionFactory };
                var accountResponse = await additional.GetApplicationContextAsyncWithHttpInfo(cancellationToken).ConfigureAwait(false);
                ValidateOrganisationAccount(accountResponse, tokenResponse.Data, organisationId);
                cancellationToken.ThrowIfCancellationRequested();
                InstallOrganisationToken(source, organisationId, tokenResponse.Data);
            }
        }

        private static bool HasExplicitOrganisationId(string json, bool account)
        {
            if (string.IsNullOrWhiteSpace(json)) return false;
            var value = JObject.Parse(json).SelectToken(account ? "organisation.id" : "organisationId");
            return value != null && value.Type != JTokenType.Null;
        }

        private static void ValidateOrganisationToken(ApiResponse<TokenResponse> response, long organisationId)
        {
            var token = response.Data;
            if (response.StatusCode != 200 || token == null || !HasExplicitOrganisationId(response.RawJsonContent, false) ||
                token.OrganisationId != organisationId || string.IsNullOrWhiteSpace(token.AccessToken) || string.IsNullOrWhiteSpace(token.Uid))
                throw new InvalidDataException("The authorization token does not confirm the requested organisation and user.");
        }

        private static void ValidateOrganisationAccount(ApiResponse<AccountInfo> response, TokenResponse token, long organisationId)
        {
            var account = response.Data;
            if (response.StatusCode != 200 || account?.Organisation == null || account.User == null ||
                !HasExplicitOrganisationId(response.RawJsonContent, true) || account.Organisation.Id != organisationId ||
                string.IsNullOrWhiteSpace(account.User.Uid) || !string.Equals(account.User.Uid, token.Uid, StringComparison.Ordinal))
                throw new InvalidDataException("The account context does not confirm the requested organisation and user.");
        }

        private void InstallOrganisationToken(Configuration source, long organisationId, TokenResponse token)
        {
            EnsureOrganisationSelection(source, organisationId);
            source.AccessToken = token.AccessToken;
            Token = token;
        }
    }
}
