﻿using System.Threading.Tasks;
using App.Services.OAuth;
using App.Utils;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace App.ViewModels
{
    public class SignInViewModel : ViewModelBase
    {
        private readonly FeedlyOAuth2Authenticator _feedlyOAuth2Authenticator;
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
        public string EmailAccountError { get; set; }
        public RelayCommand SignInCommand { get; set; }

        public SignInViewModel(FeedlyOAuth2Authenticator feedlyOAuth2Authenticator)
        {
            _feedlyOAuth2Authenticator = feedlyOAuth2Authenticator;
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

            var authToken = await _feedlyOAuth2Authenticator.Authenticate(EmailAccount, authCode);
        }

    }
}
