﻿using System.Globalization;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CurrencyMonitor.App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void FirstCurrencyValue_OnTextChangedAsync(object sender, TextChangedEventArgs e) {
            var textBox = sender as TextBox;

            decimal.TryParse(textBox.Text, out var value);         
            
            var app = Application.Current as CurrencyMonitorApp;
            var list = await app.CurrencyExchanger.GetCurrencyListAsync();

            var rub = list.First(currency => currency.CharCode == "RUB");
            var usd = list.First(currency => currency.CharCode == "USD");

            var exchanged = await app.CurrencyExchanger.Exchange(rub, usd, value);
            SecondCurrencyValue.Text = exchanged.ToString("F");
        }

        private async void SecondCurrencyValue_OnTextChangedAsync(object sender, TextChangedEventArgs e) {
            var textBox = sender as TextBox;

            decimal.TryParse(textBox.Text, out var value);         
            
            var app = Application.Current as CurrencyMonitorApp;
            var list = await app.CurrencyExchanger.GetCurrencyListAsync();

            var rub = list.First(currency => currency.CharCode == "RUB");
            var usd = list.First(currency => currency.CharCode == "USD");

            var exchanged = await app.CurrencyExchanger.Exchange(usd, rub, value);
            FirstCurrencyValue.Text = exchanged.ToString("F");
        }
    }
}
