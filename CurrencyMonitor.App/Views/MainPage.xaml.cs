using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using CurrencyMonitor.App.ViewModels;
using CurrencyMonitor.Domain;
using Microsoft.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CurrencyMonitor.App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private CurrencyInputViewModel _currencyInputViewModel;
        private CurrencySelectorViewModel _currencySelectorViewModel;

        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            base.OnNavigatedTo(e);

            InitViewModels(e);

            _currencyInputViewModel.PropertyChanged += async (sender, eventArgs) => {
                var app = Application.Current as CurrencyMonitorApp;
                var property = eventArgs.PropertyName;

                var fromCurrency = FirstCurrencyComboBox.SelectedItem as ICurrency;
                var toCurrency = SecondCurrencyComboBox.SelectedItem as ICurrency;

                if (property == nameof(_currencyInputViewModel.ToCurrencyValue)) {
                    var exchanged = await app.CurrencyExchanger.Exchange(toCurrency, fromCurrency, _currencyInputViewModel.ToCurrencyValue);
                    _currencyInputViewModel.FromCurrencyValue = exchanged;
                }
                else {
                    var exchanged = await app.CurrencyExchanger.Exchange(fromCurrency, toCurrency, _currencyInputViewModel.FromCurrencyValue);
                    _currencyInputViewModel.ToCurrencyValue = exchanged;
                }
            };
        }

        private void InitViewModels(NavigationEventArgs e) {
            _currencyInputViewModel = new CurrencyInputViewModel();
            _currencySelectorViewModel = new CurrencySelectorViewModel(e.Parameter as List<ICurrency>);

            _currencySelectorViewModel.FromCurrency = _currencySelectorViewModel.CurrencyList.First(c => c.CharCode.Equals("RUB"));
            _currencySelectorViewModel.ToCurrency = _currencySelectorViewModel.CurrencyList.First(c => c.CharCode.Equals("USD"));
        }

    }
}
