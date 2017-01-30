using System;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace App
{
    public abstract class BasePage : Page
    {
        public PageViewModel ViewModel => DataContext as PageViewModel;
        
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                var onNavigatedToTask = ViewModel?.OnNavigatedTo(e.Parameter);
                if (onNavigatedToTask != null) await onNavigatedToTask;
            });
        }

        protected override async void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                var onNavigatedFromTask = ViewModel?.OnNavigatedFrom(e.Parameter);
                if (onNavigatedFromTask != null) await onNavigatedFromTask;
            });
        }
    }
}
