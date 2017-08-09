using App.Auth;
using App.Dashboard;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;

namespace App.ViewModels
{
	public class ViewModelLocator
	{
		public static SignInViewModel SignInViewModel => SimpleIoc.Default.GetInstance<SignInViewModel>();
		public static MainViewModel MainViewModel => SimpleIoc.Default.GetInstance<MainViewModel>();

		static ViewModelLocator()
		{
			try
			{
				SimpleIoc.Default.Register<SignInViewModel>();
				SimpleIoc.Default.Register<MainViewModel>();
				SimpleIoc.Default.Register<DialogService>();
				SimpleIoc.Default.Register<INavigationService>(() =>
				{
					var navService = new NavigationService();
					navService.Configure("login-page", typeof(SignInPage));
					navService.Configure("main-page", typeof(MainPage));
					return navService;
				});
				SimpleIoc.Default.Register<Feedly.FeedlyOAuth2Authenticator>();
			}
			catch
			{

			}
		}
	}
}
