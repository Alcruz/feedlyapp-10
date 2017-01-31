using System.Threading.Tasks;
using App.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;

namespace App
{
    public abstract class PageViewModel : ViewModelBase
    {
        public INavigationService NavigationService { get; }
        public Application Application { get; }

        protected PageViewModel(INavigationService navigationService, Application application)
        {
            NavigationService = navigationService;
            Application = application;
        }

        public virtual Task OnNavigatedTo(object parameter)
        {
            return Task.CompletedTask;
        }

        public virtual Task OnNavigatedFrom(object parameter)
        {
            return Task.CompletedTask;
        }
    }
}
