using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using App.Services.OAuth;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace App.Dashboard
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await ((MainViewModel) DataContext).OnStart(e.Parameter as OAuthToken);
        }
    }
}
