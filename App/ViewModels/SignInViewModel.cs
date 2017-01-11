using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Utils;
using GalaSoft.MvvmLight;

namespace App.ViewModels
{
    public class SignInViewModel : ViewModelBase
    {
        public string EmailAccount { get; set; }
        public string Password { get; set; }
        
        public Task SignIn()
        {
            Assert.IsNotNull(EmailAccount, nameof(EmailAccount));
            Assert.IsNotNull(Password, nameof(Password));

            return Task.Delay(1000);
        }
    }
}
