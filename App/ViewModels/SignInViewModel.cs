using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using App.Utils;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace App.ViewModels
{
    public class SignInViewModel : ViewModelBase
    {
        private string _emailAccount;
        private string _password;

        public string EmailAccount
        {
            get { return _emailAccount; }
            set
            {
                Set(ref _emailAccount, value);
                SignInCommand.RaiseCanExecuteChanged();
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                Set(ref _password, value);
                SignInCommand.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand SignInCommand { get; set; }

        public SignInViewModel()
        {
            SignInCommand = new RelayCommand(async () => await SignIn(), IsSignInCommandEnabled);
        }

        private bool IsSignInCommandEnabled() =>
            !string.IsNullOrEmpty(EmailAccount) && !string.IsNullOrEmpty(Password);

        public Task SignIn()
        {
            Assert.IsNotNull(EmailAccount, nameof(EmailAccount));
            Assert.IsNotNull(Password, nameof(Password));

            return Task.Delay(1000);
        }
    }
}
