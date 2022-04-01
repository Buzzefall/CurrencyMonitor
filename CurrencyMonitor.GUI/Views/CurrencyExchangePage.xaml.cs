using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using CurrencyMonitor.GUI.ViewModels;


namespace CurrencyMonitor.GUI.Views
{
    public sealed partial class CurrencyExchangePage : Page {
        public CurrencyInputViewModel CurrencyInputViewModel { get; set; }

        public CurrencyExchangePage() {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            base.OnNavigatedTo(e);
            SetViewModels(e);
        }

        private void SetViewModels(NavigationEventArgs e) {
            CurrencyInputViewModel = e.Parameter as CurrencyInputViewModel;
        }

        private void SwitchCurrencyButton_OnClick(object sender, RoutedEventArgs e) {
            (CurrencyInputViewModel.FromCurrencySelected, CurrencyInputViewModel.ToCurrencySelected) =
                (CurrencyInputViewModel.ToCurrencySelected, CurrencyInputViewModel.FromCurrencySelected);
        }

        private void ChangeFirstCurrencyButton_OnClick(object sender, RoutedEventArgs e) {
            CurrencyInputViewModel.LastCurrencySelectedIndex = 1;
            Frame.Navigate(typeof(CurrencySelectPage), CurrencyInputViewModel, new DrillInNavigationTransitionInfo());
        }

        private void ChangeSecondCurrencyButton_OnClick(object sender, RoutedEventArgs e) {
            CurrencyInputViewModel.LastCurrencySelectedIndex = 2;
            Frame.Navigate(typeof(CurrencySelectPage), CurrencyInputViewModel, new DrillInNavigationTransitionInfo());
        }

        private void CurrencyInputs_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs e) {
            e.Cancel = e.NewText.Any(ch => !char.IsDigit(ch) && !ch.Equals('.')) ||
                       e.NewText.Count(ch => ch.Equals('.')) > 1;
        }
    }
}
