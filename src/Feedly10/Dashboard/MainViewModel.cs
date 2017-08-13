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
		private List<TreeNode> categoryTreeNodes;

		public List<TreeNode> CategoryTreeNodes
		{
			get
			{
				return this.categoryTreeNodes;
			}
			set
			{
				Set(ref this.categoryTreeNodes, value);
			}
		}

		public Stream Stream { get { return _stream; } set { Set(ref _stream, value); } }

		public ICommand FetchFeedCommand { get; set; }

		public MainViewModel(INavigationService navigationService) : base(navigationService)
		{
			FetchFeedCommand = new RelayCommand<TreeNode>(async treeNode => await FetchFeed(treeNode?.Data as UIModel));
		}

		public override async Task OnNavigatedTo(object param)
		{
			var oAuthToken = param as Feedly.OAuthToken;
			_feedlyApi = new Feedly.FeedlyApi(oAuthToken);
			var subscriptions = await _feedlyApi.GetSubscrition();

			var treeNodes = subscriptions
				.SelectMany(subscrition => subscrition.Categories)
				.Select(categoryDto => new Category(categoryDto))
				.Distinct()
				.Select(category =>
					new TreeNode
					{
						Data = category,
						IsExpanded = true
					}).ToList();

			foreach (var nextSubscription in subscriptions)
			{
				foreach (var nextCategory in nextSubscription.Categories)
				{
					var targetTreeNodes = treeNodes.Where(treeNode => (treeNode.Data as Category).Id.Equals(nextCategory.Id));
					foreach (var nextTreeNode in targetTreeNodes)
					{
						nextTreeNode.Add(new TreeNode { Data = new Subscription(nextSubscription) });
					}
				}
			}

			CategoryTreeNodes = treeNodes.OrderBy(treeNode => (treeNode.Data as Category).Label, StringComparer.CurrentCultureIgnoreCase).ToList();
			//Stream = new Stream(await FetchAllFeeds(oAuthToken));
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
