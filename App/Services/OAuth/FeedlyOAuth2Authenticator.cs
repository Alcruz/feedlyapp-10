using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;
using App.Utils;

namespace App.Services.OAuth
{
    public class FeedlyOAuth2Authenticator
    {
        private UriBuilder BuildUri(string clientId)
        {
            var uriBuilder = new UriBuilder("https://sandbox.feedly.com/v3/auth/auth");
            var queryString = new List<KeyValuePair<string, string>>
            {
                {"response_type", "code"},
                {"client_id", clientId},
                {"redirect_uri", "http://localhost"},
                {"scope", "https://cloud.feedly.com/subscriptions"}
            };

            uriBuilder.AddQueryParameters(queryString);
            return uriBuilder;
        }
        public virtual Task<OAuthToken> Auth(string authCode)
        {
            return Task.FromResult(new OAuthToken());
        }

        public virtual async Task<string> RequestAuthCode(string username)
        {
            string authCode = null;
            var uriBuilder = BuildUri(username);
            var webAuthenticationResult = await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, uriBuilder.Uri, new Uri("http://localhost"));

            if (webAuthenticationResult.ResponseStatus == WebAuthenticationStatus.Success)
            {
                var queryParams = new UriBuilder(webAuthenticationResult.ResponseData).ParsedQuery();
                queryParams.TryGetValue("code", out authCode);
            }

            return authCode;
        }
    }
}