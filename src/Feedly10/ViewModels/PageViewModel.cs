﻿using System.Threading.Tasks;
using App.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;

namespace App
{
    public abstract class PageViewModel : ViewModelBase
    {
        public INavigationService NavigationService { get; }

        protected PageViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
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
