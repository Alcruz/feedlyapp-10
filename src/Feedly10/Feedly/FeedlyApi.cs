using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Windows.Web.Http;
using App.Services.OAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace App.Dashboard
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
				request.Headers.TryAppendWithoutValidation("Authorization", $"OAuth {OAuthToken.AccessToken}");
				var response = await httpClient.SendRequestAsync(request);
				var categoriesJson = await response.Content.ReadAsStringAsync();
				var categories = JsonDeserialize<List<Category>>(categoriesJson);
				return categories;
			}
		}
		public async Task<List<Dtos.Subscription>> GetSubscrition()
		{
			using (var httpClient = new HttpClient())
			{
				var request = new HttpRequestMessage(HttpMethod.Get, new Uri(FeedlyRegistry.Subcriptions));
				request.Headers.TryAppendWithoutValidation("Authorization", $"OAuth {OAuthToken.AccessToken}");
				var response = await httpClient.SendRequestAsync(request);
				var subscriptionJson = await response.Content.ReadAsStringAsync();
				var subscrition = JsonDeserialize<List<Dtos.Subscription>>(subscriptionJson);
				return subscrition;
			}
		}
		public async Task<StreamDto> GetContent(string streamId)
		{
			using (var httpClient = new HttpClient())
			{
				var request = new HttpRequestMessage(HttpMethod.Get, new Uri(string.Format(FeedlyRegistry.StreamContent, streamId)));
				request.Headers.TryAppendWithoutValidation("Authorization", $"OAuth {OAuthToken.AccessToken}");
				var response = await httpClient.SendRequestAsync(request);
				var contentStreamJson = await response.Content.ReadAsStringAsync();
				var contentStream = JsonDeserialize<StreamDto>(contentStreamJson);
				return contentStream;
			}
		}
		private string JsonSerialize<TModel>(TModel model) => JsonConvert.SerializeObject(model, _jsonSerializerSettings);
		private TModel JsonDeserialize<TModel>(string json) => JsonConvert.DeserializeObject<TModel>(json, _jsonSerializerSettings);
	}
}
