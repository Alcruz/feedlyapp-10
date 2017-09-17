using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Views;
using App.Models;
using System.Threading;

namespace App.Dashboard
{
	public class MainViewModel : PageViewModel
	{
		private Object _lock = new object();
		private CancellationTokenSource _cancellationTokenSource;
		private Stream _stream;
		private Feedly.FeedlyApi _feedlyApi;
		private List<Category> _categories;

		public List<Category> Categories { get { return _categories; } set { Set(ref _categories, value); } }
		public Stream Stream { get { return _stream; } set { Set(ref _stream, value); } }

		public event EventHandler CategoriesLoaded;

		public MainViewModel(INavigationService navigationService) : base(navigationService)
		{
		}

		public override async Task OnNavigatedTo(object param)
		{
			var oAuthToken = param as Feedly.OAuthToken;
			_feedlyApi = new Feedly.FeedlyApi(oAuthToken);
			Categories = await FetchCategoriesForCurrentUser();
			CategoriesLoaded?.Invoke(this, new EventArgs());
		}

		internal async Task FetchFeed(UIModel uiItem)
		{
			if (uiItem == null)
			{
				return;
			}

			CancelPreviousTask();
			var streamDto = await _feedlyApi.GetContent(Uri.EscapeDataString(uiItem.Id), _cancellationTokenSource.Token);
			if (streamDto != null)
			{
				Stream = new Stream(streamDto);
			}
		}

		private void CancelPreviousTask()
		{
			lock (_lock)
			{
				if (_cancellationTokenSource != null)
				{
					_cancellationTokenSource.Cancel();
					_cancellationTokenSource.Dispose();
				}

				_cancellationTokenSource = new CancellationTokenSource();
			}
		}

		private async Task<List<Category>> FetchCategoriesForCurrentUser()
		{
			var categories = (await _feedlyApi.GetSubscrition())
				.SelectMany(subscrition => subscrition.Categories)
				.Select(categoryDto => new Category(categoryDto))
				.Distinct()
				.OrderBy(category => category.Label, StringComparer.CurrentCultureIgnoreCase).ToList();


			foreach (var nextSubscription in await _feedlyApi.GetSubscrition())
			{
				foreach (var nextCategory in nextSubscription.Categories)
				{
					var targetCategories = categories.Where(category => category.Id.Equals(nextCategory.Id));
					foreach (var targetCategory in targetCategories)
					{
						targetCategory.AddSubscription(new Subscription(nextSubscription));
					}
				}
			}

			return categories;
		}
	}
}
