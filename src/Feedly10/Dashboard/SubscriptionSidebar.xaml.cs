using App.Dashboard;
using App.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Microsoft.Toolkit.Uwp.UI;
using Microsoft.Toolkit.Uwp.UI.Extensions;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace App.Dashboard
{
	internal sealed partial class SubscriptionSidebar : UserControl
	{
		public MainViewModel ViewModel => DataContext as MainViewModel;

		public SubscriptionSidebar()
		{
			this.InitializeComponent();
		}

		private async void CategoryListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (CategoryListView.SelectedItem == null)
			{
				return;
			}

			DisableSelection(CategoryListView);
			UnselectAllListViewItems(CategoryListView.ContainerFromItem(CategoryListView.SelectedItem) as ListViewItem);
			EnableSelection(CategoryListView);
			await ViewModel.FetchFeed(CategoryListView.SelectedItem as UIModel);
		}

		private async void SubscriptionListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var subscriptionListView = sender as ListView;
			var subscription = subscriptionListView.SelectedItem as Models.Subscription;

			if (subscription == null)
			{
				return;
			}

			DisableSelection(subscriptionListView);
			UnselectAllListViewItems(subscriptionListView.ContainerFromItem(subscription) as ListViewItem);
			EnableSelection(subscriptionListView);
			await ViewModel.FetchFeed(subscription);
		}

		private void EnableSelection(params ListView[] excepts)
		{
			var listViews = CategoryListView.FindDescendants<ListView>()
				.Union(new List<ListView> { CategoryListView })
				.Except(excepts);
			foreach (var listView in listViews)
			{
				listView.SelectionMode = ListViewSelectionMode.Single;
			}
		}

		private void DisableSelection(params ListView[] excepts)
		{
			var listViews = CategoryListView.FindDescendants<ListView>()
				.Union(new List<ListView> { CategoryListView })
				.Except(excepts);
			foreach (var listView in listViews)
			{
				listView.SelectionMode = ListViewSelectionMode.None;
			}
		}

		private void UnselectAllListViewItems(params ListViewItem[] excepts)
		{
			var allListViewItems = CategoryListView.FindDescendants<ListViewItem>()
				.SelectMany(listViewItem => listViewItem.FindDescendants<ListViewItem>())
				.Union(CategoryListView.FindDescendants<ListViewItem>())
				.Except(excepts);

			foreach (var listViewItem in allListViewItems)
			{
				listViewItem.IsSelected = false;
			}
		}
	}
}
