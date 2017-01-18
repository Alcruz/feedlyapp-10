using App.Services.OAuth;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;

namespace App.ViewModels
{
    public class ViewModelLocator
    {
        public static SignInViewModel SignInViewModel => SimpleIoc.Default.GetInstance<SignInViewModel>();

        static ViewModelLocator()
        {
            SimpleIoc.Default.Register<SignInViewModel>();
            SimpleIoc.Default.Register<DialogService>();
            SimpleIoc.Default.Register(() =>
            {
                var navService = new NavigationService();
                navService.Configure(nameof(Pages.MainPage), typeof(Pages.MainPage));
                return navService;
            });
            SimpleIoc.Default.Register<FeedlyOAuth2Authenticator>();
        }
    }
}
