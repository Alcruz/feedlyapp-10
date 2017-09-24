using System;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Feedly10.App
{
	public abstract class BasePage : Page
	{
		public PageViewModel ViewModel => DataContext as PageViewModel;

		protected override async void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);
			await OnNavigatedToAsync(e);
		}

		protected override async void OnNavigatedFrom(NavigationEventArgs e)
		{
			base.OnNavigatedFrom(e);
			await OnNavigatedFromAsync(e);
		}

		private async Task OnNavigatedToAsync(NavigationEventArgs e)
		{
			var onNavigatedToTask = ViewModel?.OnNavigatedTo(e.Parameter);
			if (onNavigatedToTask != null) await onNavigatedToTask;
		}

		private async Task OnNavigatedFromAsync(NavigationEventArgs e)
		{
			var onNavigatedFromTask = ViewModel?.OnNavigatedFrom(e.Parameter);
			if (onNavigatedFromTask != null) await onNavigatedFromTask;
		}
	}
}
