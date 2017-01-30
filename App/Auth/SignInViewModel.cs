using System.Threading.Tasks;
using App.Services.OAuth;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace App.Auth
{
    public class SignInViewModel : PageViewModel
    {
        private readonly FeedlyOAuth2Authenticator _feedlyOAuth2Authenticator;
        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set { Set(ref _isLoading, value); }
        }

        public SignInViewModel(FeedlyOAuth2Authenticator feedlyOAuth2Authenticator, INavigationService navigationService) : base(navigationService)
        {
            _feedlyOAuth2Authenticator = feedlyOAuth2Authenticator;
        }

        public override async Task OnNavigatedTo(object parameter)
        {
            var authCode = await _feedlyOAuth2Authenticator.RequestAuthCode();
            if (authCode == null)
            {
                return;
            }

            IsLoading = true;
            var authToken = await _feedlyOAuth2Authenticator.Authenticate(authCode);
            IsLoading = false;

            NavigationService.NavigateTo("main-page", authToken);
        }
    }
}
