//using System.Net.Http.Headers;

//namespace AMASSKWeb.Pages.Service
//{
//    public class AuthHandler : DelegatingHandler
//    {
//        private readonly AuthState authState;

//        public AuthHandler(AuthState authState)
//        {
//            this.authState = authState;
//        }

//        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
//        {
//            //attach token
//            if (!string.IsNullOrEmpty(authState.Token))
//            {
//                request.Headers.Authorization =
//                    new AuthenticationHeaderValue("Bearer", authState.Token);
//            }

//            return base.SendAsync(request, cancellationToken);
//        }
//    }
//}

using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http.Headers;

namespace AMASSKWeb.Pages.Service
{
    public class AuthHandler : DelegatingHandler
    {
        private readonly AuthState authState;
        private readonly NavigationManager navigation;

        public AuthHandler(AuthState authState, NavigationManager navigation)
        {
            this.authState = authState;
            this.navigation = navigation;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(authState.Token))
            {
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", authState.Token);
            }

            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                authState.Logout();   // clear token safely
                navigation.NavigateTo("/", true);
            }

            return response;
        }
    }
}