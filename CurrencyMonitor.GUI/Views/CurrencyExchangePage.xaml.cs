using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using CurrencyMonitor.GUI.UserControls;
using CurrencyMonitor.GUI.ViewModels;


namespace CurrencyMonitor.GUI.Views
{
    public sealed partial class CurrencyExchangePage : Page {
        public CurrencyInputViewModel CurrencyInputViewModel { get; set; }

        public CurrencyExchangePage() {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            base.OnNavigatedTo(e);
            SetViewModels(e);
        }

        private void SetViewModels(NavigationEventArgs e) {
            CurrencyInputViewModel = e.Parameter as CurrencyInputViewModel;
        }

        private void SelectCurrencyButton_OnClick(object sender, RoutedEventArgs e) {
            var senderName = (sender as CurrencyInputControl)?.Name;
            CurrencyInputViewModel.LastCurrencySelectedIndex = senderName == nameof(FirstCurrencyInputControl) ? 1 : 2;
            Frame.Navigate(typeof(CurrencySelectPage), CurrencyInputViewModel, new EntranceNavigationTransitionInfo());
        }

        private void SwitchCurrencyButton_OnClick(object sender, RoutedEventArgs e) {
            (CurrencyInputViewModel.FromCurrencySelected, CurrencyInputViewModel.ToCurrencySelected) =
                (CurrencyInputViewModel.ToCurrencySelected, CurrencyInputViewModel.FromCurrencySelected);

            (CurrencyInputViewModel.ToCurrencyValue, CurrencyInputViewModel.FromCurrencyValue) = 
                (CurrencyInputViewModel.FromCurrencyValue, CurrencyInputViewModel.ToCurrencyValue);
        }
    }
}
