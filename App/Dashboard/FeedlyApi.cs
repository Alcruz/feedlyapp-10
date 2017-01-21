using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Web.Http;
using App.Services.OAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace App.Dashboard
{
    public class FeedlyApi
    {
        private readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        };

        public string BaseUri { get; set; }
        public OAuthToken OAuthToken { get; set; }
        public JsonSerializer JsonSerializer { get; set; }

        private string JsonSerialize<TModel>(TModel model) =>
            JsonConvert.SerializeObject(model, _jsonSerializerSettings);

        private TModel JsonDeserialize<TModel>(string json) =>
            JsonConvert.DeserializeObject<TModel>(json, _jsonSerializerSettings);

        public FeedlyApi(OAuthToken oAuthToken)
        {
            BaseUri = "https://sandbox.feedly.com/v3";
            OAuthToken = oAuthToken;

        }



        public async Task<List<Subscrition>> GetSubscrition()
        {
            using (var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Get, new Uri(BaseUri + "/subscriptions"));
                request.Headers.TryAppendWithoutValidation("Authorization", $"OAuth {OAuthToken.AccessToken}");
                var response = await httpClient.SendRequestAsync(request);
                var subscriptionJson = await response.Content.ReadAsStringAsync();
                var subscrition = JsonDeserialize<List<Subscrition>>(subscriptionJson);
                return subscrition;
            }
        }

        public async Task<StreamDto> GetContent(string streamId)
        {
            using (var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Get,
                    new Uri(BaseUri + $"/streams/{Uri.EscapeDataString(streamId)}/contents"));
                request.Headers.TryAppendWithoutValidation("Authorization", $"OAuth {OAuthToken.AccessToken}");
                var response = await httpClient.SendRequestAsync(request);
                var contentStreamJson = await response.Content.ReadAsStringAsync();
                var contentStream = JsonDeserialize<StreamDto>(contentStreamJson);
                return contentStream;
            }
        }
    }
}
