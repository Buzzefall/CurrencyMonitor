using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace CurrencyMonitor.App
{
    public sealed partial class StartLoadingPage : Page
    {
        public StartLoadingPage()
        {
            this.InitializeComponent();
            
            LoadingScreenProgressRing.IsActive = true;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e) {
            base.OnNavigatedTo(e);

            var app = Application.Current as CurrencyMonitorApp;
            var list = await app.CurrencyExchanger.GetCurrencyListAsync();

            Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
                Frame.Navigate(typeof(MainPage), list, new EntranceNavigationTransitionInfo());
            });
        }
    }
}
