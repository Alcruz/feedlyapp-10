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

namespace App.Dashboard
{
    public class MainViewModel : PageViewModel
    {
        private StreamDto _stream;
        public StreamDto Stream { get { return _stream;  } set { Set(ref _stream, value); } }

        private List<CategoryViewModel> _categories;
        private CategoryViewModel _selected;
        private FeedlyApi _feedlyApi;

        public List<CategoryViewModel> Categories {
            get { return _categories; }
            set
            {
                Set(ref _categories, value);
            }
        }

        public CategoryViewModel SelectedCategory
        {
            get { return _selected; }
            set { Set(ref _selected, value); }
        }

        public ICommand FetchFeedCommand { get; set; }

        public MainViewModel(INavigationService navigationService): base(navigationService)
        {
            FetchFeedCommand = new RelayCommand(async () => await FetchFeed());
        }

        private async Task FetchFeed()
        {
            Stream = await _feedlyApi.GetContent(SelectedCategory.Category.Id);
        }

        public override async Task OnNavigatedTo(object param)
        {
            var oAuthToken = param as OAuthToken;
            _feedlyApi = new FeedlyApi(oAuthToken);
            var categories = await _feedlyApi.GetCategories();
            var subscriptions = await _feedlyApi.GetSubscrition();

            Categories = categories.Select(category =>
            {
                var subscriptionPerCategory =
                    subscriptions.Where(
                        subscription =>
                            subscription.Categories.Any(cat => string.Equals(cat.Id, category.Id, StringComparison.Ordinal))).ToList();
                return new CategoryViewModel(category, subscriptionPerCategory);
            }).ToList();
        }
    }
}
