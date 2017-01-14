using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;
using App.Utils;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace App.ViewModels
{
    public class SignInViewModel : ViewModelBase
    {
        private string _emailAccount;

        public string EmailAccount
        {
            get { return _emailAccount; }
            set
            {
                Set(ref _emailAccount, value);
                SignInCommand.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand SignInCommand { get; set; }

        public SignInViewModel()
        {
            SignInCommand = new RelayCommand(async () => await SignIn(), IsSignInCommandEnabled);
        }

        private bool IsSignInCommandEnabled() => !string.IsNullOrEmpty(EmailAccount);

        public async Task SignIn()
        {
            Assert.IsNotNull(EmailAccount, nameof(EmailAccount));
            var uriBuilder = new UriBuilder("https://sandbox.feedly.com/v3/auth/auth");
            var queryString = new List<KeyValuePair<string, string>>
            {
                { "response_type", "code" },
                { "client_id", EmailAccount},
                { "redirect_uri",  "http://localhost" },
                { "scope", "https://cloud.feedly.com/subscriptions" }
            };

            uriBuilder.AddQueryParameters(queryString);

            var webAuthenticationResult = await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, uriBuilder.Uri, new Uri("http://localhost"));
            if (webAuthenticationResult.ResponseStatus == WebAuthenticationStatus.Success)
            {
               var queryParams = new UriBuilder(webAuthenticationResult.ResponseData).ParsedQuery();
                
            }
        }

    }

    public class OAuth2SignInMessage : MessageBase
    {
        public Uri Uri { get; set; }
    }
}
