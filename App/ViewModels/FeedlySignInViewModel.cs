using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace App.ViewModels
{
    public class FeedlySignInViewModel : ViewModelBase
    {
        private Uri _signInUri;

        public Uri SignInUri
        {
            get
            {
                return _signInUri;
            }
            set { Set(ref _signInUri, value); }
        }
    }
}
