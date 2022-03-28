using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

using CurrencyMonitor.App;

namespace CurrencyMonitor.GUI.Views
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

            var app = Application.Current as CurrencyMonitorApplication;
            var list = await app.CurrencyExchanger.GetCurrencyListAsync();

            //Frame.Navigate(typeof(MainPage), list, new EntranceNavigationTransitionInfo());

            Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
                Frame.Navigate(typeof(MainPage), list, new EntranceNavigationTransitionInfo());
            });
        }
    }
}
