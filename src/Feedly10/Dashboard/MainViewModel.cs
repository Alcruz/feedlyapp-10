using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using TreeViewControl;

namespace App.Dashboard
{
	public class MainViewModel : PageViewModel
	{
		private Stream _stream;
		private Feedly.FeedlyApi _feedlyApi;
		private List<Category> _categories;

		public List<Category> Categories { get { return _categories; } set { Set(ref _categories, value); } }
		public Stream Stream { get { return _stream; } set { Set(ref _stream, value); } }

		public ICommand FetchFeedCommand { get; set; }

		public MainViewModel(INavigationService navigationService) : base(navigationService)
		{
			FetchFeedCommand = new RelayCommand<object>(async treeNode => await FetchFeed(treeNode as UIModel));
		}

		public override async Task OnNavigatedTo(object param)
		{
			var oAuthToken = param as Feedly.OAuthToken;
			_feedlyApi = new Feedly.FeedlyApi(oAuthToken);
			var subscriptions = await _feedlyApi.GetSubscrition();

			var categories = subscriptions
				.SelectMany(subscrition => subscrition.Categories)
				.Select(categoryDto => new Category(categoryDto))
				.Distinct()
				.ToList();

			foreach (var nextSubscription in subscriptions)
			{
				var subscription = new Subscription(nextSubscription);
				foreach (var nextCategory in nextSubscription.Categories)
				{
					
					var targetCategories = categories.Where(category => category.Id.Equals(nextCategory.Id));
					foreach (var targetCategory in targetCategories)
					{
						targetCategory.AddSubscription(subscription);
					}
				}
			}

			Categories = categories.OrderBy(category => category.Label, StringComparer.CurrentCultureIgnoreCase).ToList();
		}

		private async Task FetchFeed(UIModel uiItem)
		{
			if (uiItem == null)
			{
				return;
			}

			var streamDto = await _feedlyApi.GetContent(Uri.EscapeDataString(uiItem.Id));
			Stream = new Stream(streamDto);
		}

		private async Task<Feedly.Stream> FetchAllFeeds(Feedly.OAuthToken oAuthToken) => 
			await _feedlyApi.GetContent($"user/{oAuthToken.AccessToken}/category/global.all");
	}
}
