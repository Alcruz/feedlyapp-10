using System.Threading.Tasks;
using App.Services.OAuth;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace App.Auth
{
    public class SignInViewModel : ViewModelBase
    {
        private readonly FeedlyOAuth2Authenticator _feedlyOAuth2Authenticator;
        private readonly INavigationService _navigationService;
        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set { Set(ref _isLoading, value); }
        }

        public RelayCommand SignInCommand { get; set; }

        public SignInViewModel(FeedlyOAuth2Authenticator feedlyOAuth2Authenticator, INavigationService navigationService)
        {
            _feedlyOAuth2Authenticator = feedlyOAuth2Authenticator;
            _navigationService = navigationService;
            SignInCommand = new RelayCommand(async () => await SignIn(), IsSignInCommandEnabled);
        }

        private bool IsSignInCommandEnabled() => !IsLoading;

        public async Task SignIn()
        {
            var authCode = await _feedlyOAuth2Authenticator.RequestAuthCode();
            if (authCode == null)
            {
                return;
            }

            IsLoading = true;
            var authToken = await _feedlyOAuth2Authenticator.Authenticate(authCode);
            IsLoading = false;

            _navigationService.NavigateTo("main-page", authToken);
        }

    }
}
