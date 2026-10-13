using System.Threading;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi
{
    public partial class OpenScopeApiClient
    {
        private readonly SemaphoreSlim asynchronousAuthorization = new SemaphoreSlim(1, 1);

        /// <summary>Authorizes the configured user using cancellable asynchronous HTTP I/O.</summary>
        /// <param name="cancellationToken">Cancels waiting or the active authorization request.</param>
        /// <returns>A task representing authorization and token installation.</returns>
        /// <remarks>A configured numeric organisation identifier takes precedence over its name. Explicit-ID authorization validates the token and a fresh account context before installation, without an organisation lookup or default fallback. An unset identifier retains the existing single token request.</remarks>
        /// <exception cref="System.IO.InvalidDataException">An explicit organisation's token or account context is missing or inconsistent.</exception>
        /// <exception cref="System.InvalidOperationException">The explicit organisation selection changes before token installation.</exception>
        public async Task AuthorizeWithUserCredentialsAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var source = Config;
            var organisationId = source.OrganisationId;
            await asynchronousAuthorization.WaitAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                if (organisationId.HasValue)
                {
                    await AuthorizeOrganisationAsync(source, organisationId.Value, null, null, cancellationToken).ConfigureAwait(false);
                    return;
                }
                var response = await AuthorizationApi.TokenAsyncWithHttpInfo("password", Config.ClientNumber, cancellationToken,
                    username: Config.Username, organisation: Config.OrganisationName, password: Config.Password).ConfigureAwait(false);
                Token = response.Data;
            }
            finally { asynchronousAuthorization.Release(); }
        }

        /// <summary>Refreshes installed authorization tokens using cancellable asynchronous HTTP I/O.</summary>
        /// <param name="cancellationToken">Cancels waiting or the active token-refresh request.</param>
        /// <returns>A task representing refresh and token installation.</returns>
        /// <exception cref="System.InvalidOperationException">No refresh token is installed, or the explicit organisation selection changes before installation.</exception>
        /// <remarks>With a configured numeric organisation identifier, the installed token must belong to that organisation and the refreshed token and fresh account context are validated before installation. An unset identifier retains existing refresh behavior.</remarks>
        /// <exception cref="System.IO.InvalidDataException">The installed or refreshed identity does not confirm the configured organisation and user.</exception>
        public async Task RefreshAuthorizationAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var source = Config;
            var organisationId = source.OrganisationId;
            await asynchronousAuthorization.WaitAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                if (Token == null || string.IsNullOrEmpty(Token.RefreshToken))
                    throw new System.InvalidOperationException("Authorization must provide a refresh token before refresh is requested.");
                if (organisationId.HasValue)
                {
                    if (Token.OrganisationId != organisationId.Value || string.IsNullOrWhiteSpace(Token.Uid))
                        throw new System.IO.InvalidDataException("The installed token does not confirm the configured organisation.");
                    await AuthorizeOrganisationAsync(source, organisationId.Value, Token.RefreshToken, Token.Uid, cancellationToken).ConfigureAwait(false);
                    return;
                }
                var response = await AuthorizationApi.TokenAsyncWithHttpInfo("refresh_token", Config.ClientNumber, cancellationToken,
                    refreshToken: Token.RefreshToken).ConfigureAwait(false);
                Token = response.Data;
            }
            finally { asynchronousAuthorization.Release(); }
        }
    }
}
