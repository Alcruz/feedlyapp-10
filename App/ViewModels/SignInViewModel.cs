using System.Threading.Tasks;
using App.Services.OAuth;
using App.Utils;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace App.ViewModels
{
    public class SignInViewModel : ViewModelBase
    {
        private readonly FeedlyOAuth2Authenticator _feedlyOAuth2Authenticator;
        private readonly INavigationService _navigationService;
        private string _emailAccount;
        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set { Set(ref _isLoading, value); }
        }

        public string EmailAccount
        {
            get { return _emailAccount; }
            set
            {
                Set(ref _emailAccount, value);
                SignInCommand.RaiseCanExecuteChanged();
            }
        }
        public string EmailAccountError { get; set; }
        public RelayCommand SignInCommand { get; set; }

        public SignInViewModel(FeedlyOAuth2Authenticator feedlyOAuth2Authenticator, INavigationService navigationService)
        {
            _feedlyOAuth2Authenticator = feedlyOAuth2Authenticator;
            _navigationService = navigationService;
            SignInCommand = new RelayCommand(async () => await SignIn(), IsSignInCommandEnabled);
        }

        private bool IsSignInCommandEnabled() => !string.IsNullOrEmpty(EmailAccount);

        public async Task SignIn()
        {
            Assert.IsNotNull(EmailAccount, nameof(EmailAccount));
            var doesAccountExist = await _feedlyOAuth2Authenticator.ValidateAccount(EmailAccount);
            if (!doesAccountExist)
            {
                EmailAccountError = "Invalid account";
                return;
            }

            var authCode = await _feedlyOAuth2Authenticator.RequestAuthCode(EmailAccount);
            if (authCode == null)
            {
                EmailAccountError = "Invalid credentials";
                return;
            }

            IsLoading = true;
            var authToken = await _feedlyOAuth2Authenticator.Authenticate(EmailAccount, authCode);
            IsLoading = false;
        }

    }
}
