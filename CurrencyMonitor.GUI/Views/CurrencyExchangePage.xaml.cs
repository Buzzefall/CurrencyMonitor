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
    public sealed partial class CurrencyExchangePage : Page
    {
        public CurrencyInputViewModel CurrencyInputViewModel { get; set; }

        public CurrencyExchangePage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            SetNumberBoxFormatters();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            base.OnNavigatedTo(e);
            SetViewModels(e);
        }

        private void SetViewModels(NavigationEventArgs e) {
            CurrencyInputViewModel = e.Parameter as CurrencyInputViewModel;
            CurrencyInputViewModel.PropertyChanged += ProcessTriggers;
        }

        private async void ProcessTriggers(object sender, PropertyChangedEventArgs eventArgs) {
            var app = Application.Current as CurrencyMonitorApplication;
            var propertyName = eventArgs.PropertyName;

            var fromCurrency = FirstCurrencyComboBox.SelectedItem as ICurrency;
            var toCurrency = SecondCurrencyComboBox.SelectedItem as ICurrency;

            if (propertyName == nameof(CurrencyInputViewModel.ToCurrencySelectedValue) ||
                propertyName == nameof(CurrencyInputViewModel.ToCurrencySelected)) {
                var exchanged = await app.CurrencyExchanger.Exchange(toCurrency, fromCurrency, CurrencyInputViewModel.ToCurrencySelectedValue);
                CurrencyInputViewModel.FromCurrencySelectedValue = exchanged;
            }
            else {
                var exchanged = await app.CurrencyExchanger.Exchange(fromCurrency, toCurrency, CurrencyInputViewModel.FromCurrencySelectedValue);
                CurrencyInputViewModel.ToCurrencySelectedValue = exchanged;
            }

        }

        private void SetNumberBoxFormatters() {
            var rounder = new IncrementNumberRounder {
                Increment = 0.5,
                RoundingAlgorithm = RoundingAlgorithm.RoundUp
            };

            var formatter = new DecimalFormatter {
                IntegerDigits = 1,
                FractionDigits = 2,
                NumberRounder = rounder
            };

            FirstCurrencyInput.NumberFormatter = formatter;
            SecondCurrencyInput.NumberFormatter = formatter;
        }

        private void SwitchCurrencyButton_OnClick(object sender, RoutedEventArgs e) {
            var (from, to) = (CurrencyInputViewModel.FromCurrencySelected, CurrencyInputViewModel.ToCurrencySelected);
            (from, to) = (to, from); // TODO: check 
        }
    }
}
