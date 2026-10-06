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
        public async Task AuthorizeWithUserCredentialsAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await asynchronousAuthorization.WaitAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                var response = await AuthorizationApi.TokenAsyncWithHttpInfo("password", Config.ClientNumber, cancellationToken,
                    username: Config.Username, organisation: Config.OrganisationName, password: Config.Password).ConfigureAwait(false);
                Token = response.Data;
            }
            finally { asynchronousAuthorization.Release(); }
        }

        /// <summary>Refreshes installed authorization tokens using cancellable asynchronous HTTP I/O.</summary>
        /// <param name="cancellationToken">Cancels waiting or the active token-refresh request.</param>
        /// <returns>A task representing refresh and token installation.</returns>
        /// <exception cref="System.InvalidOperationException">No refresh token is installed.</exception>
        public async Task RefreshAuthorizationAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await asynchronousAuthorization.WaitAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                if (Token == null || string.IsNullOrEmpty(Token.RefreshToken))
                    throw new System.InvalidOperationException("Authorization must provide a refresh token before refresh is requested.");
                var response = await AuthorizationApi.TokenAsyncWithHttpInfo("refresh_token", Config.ClientNumber, cancellationToken,
                    refreshToken: Token.RefreshToken).ConfigureAwait(false);
                Token = response.Data;
            }
            finally { asynchronousAuthorization.Release(); }
        }
    }
}
