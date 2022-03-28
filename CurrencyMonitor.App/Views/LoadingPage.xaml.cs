using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CurrencyMonitor.App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoadingPage : Page
    {
        public LoadingPage()
        {
            this.InitializeComponent();
            
            LoadingScreenProgressRing.IsActive = true;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e) {
            base.OnNavigatedTo(e);

            var app = Application.Current as CurrencyMonitorApp;
            var list = await app.CurrencyExchanger.GetCurrencyListAsync();

            Frame.Navigate(typeof(MainPage), list, new EntranceNavigationTransitionInfo());
        }
    }
}
