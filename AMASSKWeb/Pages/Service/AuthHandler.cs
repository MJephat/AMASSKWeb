using System.Net.Http.Headers;

namespace AMASSKWeb.Pages.Service
{
    public class AuthHandler : DelegatingHandler
    {
        private readonly AuthState authState;

        public AuthHandler(AuthState authState)
        {
            this.authState = authState;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(authState.Token))
            {
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", authState.Token);
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}