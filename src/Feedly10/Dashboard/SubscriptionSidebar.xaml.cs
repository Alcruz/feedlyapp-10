using App.Models;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Controls;
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
			ChangeSelectedItems(CategoryListView);
			await ViewModel.FetchFeed(CategoryListView.SelectedItem as UIModel);
		}

		private async void SubscriptionListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ChangeSelectedItems(sender as ListView);
			await ViewModel.FetchFeed((sender as ListView).SelectedItem as UIModel);
		}

		private void ChangeSelectedItems(ListView listView)
		{
			if (listView.SelectedItem == null)
			{
				return;
			}

			DisableSelection(listView);
			UnselectAllListViewItems(listView.ContainerFromItem(listView.SelectedItem) as ListViewItem);
			EnableSelection(listView);
		}
		
		private void EnableSelection(params ListView[] excepts)
		{
			SetListViewSelectionMode(ListViewSelectionMode.Single, excepts);
		}

		private void DisableSelection(params ListView[] excepts)
		{
			SetListViewSelectionMode(ListViewSelectionMode.None, excepts);
		}

		private void SetListViewSelectionMode(ListViewSelectionMode listViewSelectionMode, params ListView[] excepts)
		{
			var listViews = CategoryListView.FindDescendants<ListView>()
							.Union(new List<ListView> { CategoryListView })
							.Except(excepts);
			foreach (var listView in listViews)
			{
				listView.SelectionMode = listViewSelectionMode;
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
