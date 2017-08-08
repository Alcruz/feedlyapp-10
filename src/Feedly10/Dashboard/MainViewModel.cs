using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using App.Services;
using App.Services.OAuth;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using TreeViewControl;

namespace App.Dashboard
{
	public class MainViewModel : PageViewModel
	{
		private StreamDto _stream;
		private FeedlyApi _feedlyApi;
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

		public StreamDto Stream { get { return _stream; } set { Set(ref _stream, value); } }

		public ICommand FetchFeedCommand { get; set; }

		public MainViewModel(INavigationService navigationService) : base(navigationService)
		{
			FetchFeedCommand = new RelayCommand(() => { });
		}

		public override async Task OnNavigatedTo(object param)
		{
			var oAuthToken = param as OAuthToken;
			_feedlyApi = new FeedlyApi(oAuthToken);
			var subscriptions = await _feedlyApi.GetSubscrition();

			var treeNodes = subscriptions.SelectMany(subscrition => subscrition.Categories).Distinct().Select(category =>
			new TreeNode
			{
				Data = category,
				IsExpanded = true
			}).ToList();

			foreach (var nextSubscription in subscriptions)
			{
				foreach (var nextCategory in nextSubscription.Categories)
				{
					var targetTreeNodes = treeNodes.Where(treeNode => treeNode.Data.Equals(nextCategory));
					foreach (var nextTreeNode in targetTreeNodes)
					{
						nextTreeNode.Add(new TreeNode { Data = new Subscription(nextSubscription) });
					}
				}
			}

			CategoryTreeNodes = treeNodes.OrderBy(treeNode => (treeNode.Data as Category).Label, StringComparer.CurrentCultureIgnoreCase).ToList();
			await FetchAllFeeds(oAuthToken);
		}

		private async Task FetchAllFeeds(OAuthToken oAuthToken)
		{
			var stream = await _feedlyApi.GetContent($"user/{oAuthToken.AccessToken}/category/global.all");
		
		}
	}
}
