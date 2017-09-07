using App.Models;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Microsoft.Toolkit.Uwp.UI.Extensions;
using System.Threading;
using System;
using System.Threading.Tasks;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace App.Dashboard
{
	internal sealed partial class SubscriptionSidebar : UserControl
	{
		public MainViewModel ViewModel => DataContext as MainViewModel;
		
		public SubscriptionSidebar()
		{
			this.InitializeComponent();
			this.Loaded += SubscriptionSidebar_Loaded;
		}

		private void SubscriptionSidebar_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs eventArgs)
		{
			ViewModel.CategoriesLoaded += (s, e) => CategoryListView.SelectedIndex = 0; 
		}

		private async void CategoryListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			await ExecuteChangeSelection(CategoryListView);
		}

		private async void SubscriptionListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			await ExecuteChangeSelection(sender as ListView);
		}

		private async Task ExecuteChangeSelection(ListView listView)
		{
			if (listView.SelectedItem == null)
			{
				return;
			}

			DisableSelection(listView);
			UnselectAllListViewItems(listView.ContainerFromItem(listView.SelectedItem) as ListViewItem);
			EnableSelection(listView);
			await ViewModel.FetchFeed(listView.SelectedItem as UIModel);
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
