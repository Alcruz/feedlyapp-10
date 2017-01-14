using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using App.ViewModels;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;

namespace App
{
    public class ViewModelLocator
    {
        public static FeedlySignInViewModel FeedlySignInViewModel => SimpleIoc.Default.GetInstance<FeedlySignInViewModel>();
        public static SignInViewModel SignInViewModel => SimpleIoc.Default.GetInstance<SignInViewModel>();
        public static INavigationService NavigationService => SimpleIoc.Default.GetInstance<NavigationService>();
        public static IDialogService DialogService => SimpleIoc.Default.GetInstance<DialogService>();

        static ViewModelLocator()
        {
            SimpleIoc.Default.Register<FeedlySignInViewModel>();
            SimpleIoc.Default.Register<SignInViewModel>();
            SimpleIoc.Default.Register<DialogService>();
            SimpleIoc.Default.Register(() =>
            {
                var navService = new NavigationService();
                navService.Configure(nameof(FeedlySignInPage), typeof(FeedlySignInPage));
                navService.Configure(nameof(MainPage), typeof(MainPage));
                return navService;
            });


            Messenger.Default.Register<OAuth2SignInMessage>(FeedlySignInViewModel, oAuth2SignInMessage =>
            {
                NavigationService.NavigateTo(nameof(FeedlySignInPage), oAuth2SignInMessage);
            });
        }
    }
}
