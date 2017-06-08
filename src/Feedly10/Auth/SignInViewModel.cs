using System.Threading.Tasks;
using App.Services.OAuth;
using GalaSoft.MvvmLight.Views;
using Windows.ApplicationModel.Core;
using App.Services;
using System;

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

			var authCode = GetAuthCodeFromLocalStore();

			if (authCode == null)
			{
				authCode = await _feedlyOAuth2Authenticator.RequestAuthCode();
			}

			if (authCode == null)
			{
				CoreApplication.Exit();
			}

			IsLoading = true;
			SaveAuthCodeToLocalStore(authCode);
			var authToken = await _feedlyOAuth2Authenticator.Authenticate(authCode);
			IsLoading = false;

			NavigationService.NavigateTo("main-page", authToken);
		}

		private string GetAuthCodeFromLocalStore()
		{
			Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
			return (string) localSettings.Values["authCode"];
		}

		private void SaveAuthCodeToLocalStore(string authCode)
		{
			Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
			localSettings.Values["authCode"] = authCode;
		}
	}
}
