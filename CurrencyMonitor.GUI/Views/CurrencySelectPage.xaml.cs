using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using CurrencyMonitor.Domain.Entities;
using CurrencyMonitor.GUI.ViewModels;


namespace CurrencyMonitor.GUI.Views
{
    
    public sealed partial class CurrencySelectPage : Page
    {
        private CurrencyInputViewModel CurrencyInputViewModel { get; set; }

        public CurrencySelectPage()
        {
            this.InitializeComponent();
            this.Loading += OnLoadingStart;
        }

        private void OnLoadingStart(FrameworkElement frameworkElement, object args) {
            CurrencySelectList.SelectedItem =
                CurrencyInputViewModel.LastCurrencySelectedIndex == 1
                    ? CurrencyInputViewModel.FromCurrencySelected
                    : CurrencyInputViewModel.ToCurrencySelected;
        }


        protected override void OnNavigatedTo(NavigationEventArgs e) {
            base.OnNavigatedTo(e);

            CurrencyInputViewModel = e.Parameter as CurrencyInputViewModel;
        }


        private void CurrencySelectList_OnItemClick(object sender, ItemClickEventArgs e) {
            var selected = e.ClickedItem as ICurrency;

            switch (CurrencyInputViewModel.LastCurrencySelectedIndex) {
                case 1 when CurrencyInputViewModel.FromCurrencySelected != selected:
                    CurrencyInputViewModel.FromCurrencySelected = selected;
                    break;
                case 2 when CurrencyInputViewModel.ToCurrencySelected != selected:
                    CurrencyInputViewModel.ToCurrencySelected = selected;
                    break;
                default:
                    return;
            }

            Frame.Navigate(typeof(CurrencyExchangePage), CurrencyInputViewModel, new DrillInNavigationTransitionInfo());
        }
    }
}
