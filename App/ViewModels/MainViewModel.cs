using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;
using App.Services.OAuth;
using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace App.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private List<Subscrition> _subscription;

        public List<Subscrition> Subscrition {
            get { return _subscription; }
            set
            {
                Set(ref _subscription, value);
                
            }
        }

        public async Task OnStart(OAuthToken oAuthToken)
        {
            using (var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Get, new Uri("https://sandbox.feedly.com/v3/subscriptions"));
                request.Headers.TryAppendWithoutValidation("Authorization", $"OAuth {oAuthToken.AccessToken}");
                var response = await httpClient.SendRequestAsync(request);
                var subscriptionJson = await response.Content.ReadAsStringAsync();
                var subscrition = JsonConvert.DeserializeObject<List<Subscrition>>(subscriptionJson,
                    new JsonSerializerSettings
                    {
                        ContractResolver = new DefaultContractResolver
                        {
                            NamingStrategy = new SnakeCaseNamingStrategy()
                        }
                    });

                Subscrition = subscrition;
            }
        }

    }
}
