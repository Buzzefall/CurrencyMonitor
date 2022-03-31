using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using Windows.Globalization.NumberFormatting;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

using Microsoft.UI.Xaml.Controls;

using CurrencyMonitor.Domain.Entities;
using CurrencyMonitor.GUI.ViewModels;
using CurrencyMonitor.GUI.Helpers;


namespace CurrencyMonitor.GUI.Views
{
    public sealed partial class CurrencyExchangePage : Page {
        public CurrencyInputViewModel CurrencyInputViewModel { get; set; }

        public CurrencyExchangePage() {
            this.InitializeComponent();
            //this.NavigationCacheMode = NavigationCacheMode.Required;
            SetNumberBoxFormatters();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            base.OnNavigatedTo(e);
            SetViewModels(e);
            Focus(FocusState.Keyboard);
        }

        private void SetViewModels(NavigationEventArgs e) {
            CurrencyInputViewModel = e.Parameter as CurrencyInputViewModel;
            CurrencyInputViewModel.PropertyChanged += ProcessTriggers;
        }

        private async void ProcessTriggers(object sender, PropertyChangedEventArgs eventArgs) {
            var app = Application.Current as CurrencyMonitorApplication;
            var propertyName = eventArgs.PropertyName;

            var fromCurrency = CurrencyInputViewModel.FromCurrencySelected as ICurrency;
            var toCurrency = CurrencyInputViewModel.ToCurrencySelected as ICurrency;

            if (propertyName == nameof(CurrencyInputViewModel.ToCurrencySelectedValue) ||
                propertyName == nameof(CurrencyInputViewModel.ToCurrencySelected)) {
                var exchanged = app.CurrencyExchangerServiceService.Exchange(toCurrency, fromCurrency,
                    CurrencyInputViewModel.ToCurrencySelectedValue);
                CurrencyInputViewModel.FromCurrencySelectedValue = exchanged;
            }
            else {
                var exchanged = app.CurrencyExchangerServiceService.Exchange(fromCurrency, toCurrency,
                    CurrencyInputViewModel.FromCurrencySelectedValue);
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

            //FirstCurrencyInput.NumberFormatter = formatter;
            //SecondCurrencyInput.NumberFormatter = formatter;
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

        private void CurrencyInputs_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args) {
            double.TryParse(sender.Text, out var number);
            if (number >= 0.0) {
                sender.Text = number.ToString("F");
            }
        }
    }
}
