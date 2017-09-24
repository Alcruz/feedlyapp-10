using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Threading;
using System.Net.Http;

namespace Feedly10.App.Feedly
{
	public class FeedlyApi
	{
		public FeedlyApi(OAuthToken oAuthToken)
		{
			OAuthToken = oAuthToken;
		}

		public string BaseUri { get; }
		public OAuthToken OAuthToken { get; }
		public JsonSerializer JsonSerializer { get; }

		private readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
		{
			ContractResolver = new DefaultContractResolver
			{
				NamingStrategy = new SnakeCaseNamingStrategy()
			}
		};

		public async Task<List<Category>> GetCategories()
		{
			using (var httpClient = new HttpClient())
			{
				var request = new HttpRequestMessage(HttpMethod.Get, new Uri(FeedlyRegistry.Categories));
				request.Headers.TryAddWithoutValidation("Authorization", $"OAuth {OAuthToken.AccessToken}");
				var response = await httpClient.SendAsync(request);
				var categoriesJson = await response.Content.ReadAsStringAsync();
				var categories = JsonDeserialize<List<Category>>(categoriesJson);
				return categories;
			}
		}
		public async Task<List<Subscription>> GetSubscrition()
		{
			using (var httpClient = new HttpClient())
			{
				var request = new HttpRequestMessage(HttpMethod.Get, new Uri(FeedlyRegistry.Subcriptions));
				request.Headers.TryAddWithoutValidation("Authorization", $"OAuth {OAuthToken.AccessToken}");
				var response = await httpClient.SendAsync(request);
				var subscriptionJson = await response.Content.ReadAsStringAsync();
				var subscrition = JsonDeserialize<List<Subscription>>(subscriptionJson);
				return subscrition;
			}
		}
		public async Task<Stream> GetContent(string streamId, CancellationToken cancellationToken)
		{
			try
			{
				using (var httpClient = new HttpClient())
				{
					var request = new HttpRequestMessage(HttpMethod.Get, new Uri(string.Format(FeedlyRegistry.StreamContent, streamId)));
					request.Headers.TryAddWithoutValidation("Authorization", $"OAuth {OAuthToken.AccessToken}");
					var response = await httpClient.SendAsync(request, cancellationToken);
					var contentStreamJson = await response.Content.ReadAsStringAsync();
					var contentStream = JsonDeserialize<Stream>(contentStreamJson);
					return contentStream;
				}
			}
			catch (TaskCanceledException ex)
			{
				if (!cancellationToken.IsCancellationRequested)
				{
					throw ex;
				}
			}

			return null;
		}
		private string JsonSerialize<TModel>(TModel model) => JsonConvert.SerializeObject(model, _jsonSerializerSettings);
		private TModel JsonDeserialize<TModel>(string json) => JsonConvert.DeserializeObject<TModel>(json, _jsonSerializerSettings);
	}
}
