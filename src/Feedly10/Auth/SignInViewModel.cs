using System.Threading.Tasks;
using App.Services.OAuth;
using GalaSoft.MvvmLight.Views;
using Windows.ApplicationModel.Core;
using App.Services;
using System;
using App.Utils;

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
#if DEBUG
			await Windows.Storage.ApplicationData.Current.ClearAsync();
#endif
			var authTokenPair = GetAuthTokenFromLocalStore();

			if (authTokenPair.result && !authTokenPair.token.HasExpire())
			{
				NavigationService.NavigateTo("main-page", authTokenPair.token);
				return;
			}

			string authCode = await _feedlyOAuth2Authenticator.RequestAuthCode();

			if (authCode == null)
			{
				CoreApplication.Exit();
				return;
			}

			IsLoading = true;


			var authToken = await _feedlyOAuth2Authenticator.Authenticate(authCode);

			if (authToken == null)
			{
				CoreApplication.Exit();
				return;
			}

			SaveAuthCodeToLocalStore(authToken);


			IsLoading = false;

			NavigationService.NavigateTo("main-page", authToken);
		}

		private (bool result, OAuthToken token) GetAuthTokenFromLocalStore()
		{
			Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
			var composite = (Windows.Storage.ApplicationDataCompositeValue) localSettings.Values["AuthToken"];

			if (composite == null)
			{
				return (result: false, token: null);
			}

			return (
				result: true,
				token: new OAuthToken
				{
					AccessToken = (string)composite["AccessToken"],
					ExpiresIn = (int)composite["ExpiresIn"],
					Id = (string)composite["Id"],
					Plan = (string)composite["Plan"],
					RefreshToken = (string)composite["RefreshToken"],
					State = (string)composite["State"],
					TokenType = (string)composite["TokenType"],
					CreatedAt = (DateTimeOffset)composite["CreatedAt"]
				}
			);
		}

		private void SaveAuthCodeToLocalStore(OAuthToken authToken)
		{
			Assert.IsNotNull(authToken, "AuthToken");
			var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
			var composite = new Windows.Storage.ApplicationDataCompositeValue
			{
				["AccessToken"] = authToken.AccessToken,
				["ExpiresIn"] = authToken.ExpiresIn,
				["Id"] = authToken.Id,
				["Plan"] = authToken.Plan,
				["RefreshToken"] = authToken.RefreshToken,
				["State"] = authToken.State ?? string.Empty,
				["TokenType"] = authToken.TokenType,
				["CreatedAt"] = authToken.CreatedAt
			};
			localSettings.Values["AuthToken"] = composite;
		}
	}
}
