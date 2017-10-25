using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Feedly10.App.Utils;

namespace Feedly10.App.Feedly
{
	public class FeedlyOAuth2Authenticator
	{

		private UriBuilder BuildUri()
		{
			var uriBuilder = new UriBuilder(FeedlyRegistry.AuthCodeUrl);
			var queryString = new List<KeyValuePair<string, string>>
			{
				{"response_type", "code"},
				{"client_id", "sandbox"},
				{"redirect_uri", FeedlyRegistry.AuthRedirectUrl},
				{"scope", FeedlyRegistry.ScopeSubscription}
			};

			uriBuilder.AddQueryParameters(queryString);
			return uriBuilder;
		}
		public virtual async Task<OAuthToken> Authenticate(string authCode)
		{
			using (var httpClient = new HttpClient())
			{
				var request = new Dictionary<string, string>()
				{
					["code"] = authCode,
					["client_id"] = "sandbox",
					["client_secret"] = "g8PThNUyMwy5ZJBw",
					["redirect_uri"] = FeedlyRegistry.AuthRedirectUrl,
					["grant_type"] = "authorization_code"
				};
				var json = JsonConvert.SerializeObject(request, new JsonSerializerSettings
				{
					ContractResolver = new CamelCasePropertyNamesContractResolver()
				});

				var content = new StringContent(json, Encoding.UTF8, "application/json");
				var response = await httpClient.PostAsync(FeedlyRegistry.AuthTokenUrl, content);
				if (!response.IsSuccessStatusCode) return null;

				var authTokenJson = await response.Content.ReadAsStringAsync();
				var authToken = JsonConvert.DeserializeObject<OAuthToken>(authTokenJson, new JsonSerializerSettings
				{
					ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() }
				});
				authToken.CreatedAt = new DateTimeOffset(response.Headers.Date?.UtcTicks ?? DateTimeOffset.UtcNow.UtcTicks, TimeSpan.Zero);
				return authToken;
			}
		}

		public virtual async Task<string> RequestAuthCode()
		{
			string authCode = null;
			var uriBuilder = BuildUri();
			var webAuthenticationResult = await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, uriBuilder.Uri, new Uri(FeedlyRegistry.AuthRedirectUrl));

			if (webAuthenticationResult.ResponseStatus == WebAuthenticationStatus.Success)
			{
				var queryParams = new UriBuilder(webAuthenticationResult.ResponseData).ParsedQuery();
				queryParams.TryGetValue("code", out authCode);
			}

			return authCode;
		}
	}
}