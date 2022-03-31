using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using Windows.Globalization.NumberFormatting;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using Microsoft.UI.Xaml.Controls;

using CurrencyMonitor.Domain.Entities;
using CurrencyMonitor.GUI.ViewModels;
using CurrencyMonitor.GUI.Helpers;


namespace CurrencyMonitor.GUI.Views
{
    public sealed partial class MainPage : Page
    {
        public CurrencyInputViewModel _currencyInputViewModel;
        public CurrencySelectorViewModel _currencySelectorViewModel;

        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            base.OnNavigatedTo(e);

            InitViewModels(e);
            SetNumberBoxFormatters();

            _currencyInputViewModel.PropertyChanged += ProcessTriggers;
            _currencySelectorViewModel.PropertyChanged += ProcessTriggers;
        }


        private void SwitchCurrencyButton_OnClick(object sender, RoutedEventArgs e) {
            (_currencySelectorViewModel.FromCurrency, _currencySelectorViewModel.ToCurrency) =
                (_currencySelectorViewModel.ToCurrency, _currencySelectorViewModel.FromCurrency);
        }

        private void InitViewModels(NavigationEventArgs e) {
            _currencyInputViewModel = new CurrencyInputViewModel();
            _currencySelectorViewModel = new CurrencySelectorViewModel(e.Parameter as List<ICurrency>);

            _currencySelectorViewModel.FromCurrency = _currencySelectorViewModel.CurrencyList.First(c => c.CharCode.Equals("RUB"));
            _currencySelectorViewModel.ToCurrency = _currencySelectorViewModel.CurrencyList.First(c => c.CharCode.Equals("USD"));
        }

        private async void ProcessTriggers(object sender, PropertyChangedEventArgs eventArgs) {
            var app = Application.Current as CurrencyMonitorApplication;
            var property = eventArgs.PropertyName;

            var fromCurrency = FirstCurrencyComboBox.SelectedItem as ICurrency;
            var toCurrency = SecondCurrencyComboBox.SelectedItem as ICurrency;

            if (property == nameof(_currencyInputViewModel.ToCurrencyValue) ||
                property == nameof(_currencySelectorViewModel.ToCurrency)) {
                var exchanged = await app.CurrencyExchanger.Exchange(toCurrency, fromCurrency, _currencyInputViewModel.ToCurrencyValue);
                _currencyInputViewModel.FromCurrencyValue = exchanged;
            }
            else {
                var exchanged = await app.CurrencyExchanger.Exchange(fromCurrency, toCurrency, _currencyInputViewModel.FromCurrencyValue);
                _currencyInputViewModel.ToCurrencyValue = exchanged;
            }

        }

        private void SetNumberBoxFormatters() {
            IncrementNumberRounder rounder = new IncrementNumberRounder();
            rounder.Increment = 0.5;
            rounder.RoundingAlgorithm = RoundingAlgorithm.RoundUp;

            DecimalFormatter formatter = new DecimalFormatter();
            formatter.IntegerDigits = 1;
            formatter.FractionDigits = 2;
            formatter.NumberRounder = rounder;

            FirstCurrencyInput.NumberFormatter = formatter;
            SecondCurrencyInput.NumberFormatter = formatter;
        }
    }
}
