using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using CurrencyMonitor.App.ViewModels;
using CurrencyMonitor.Logic.Interfaces;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CurrencyMonitor.App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private CurrencyInputViewModel _viewModel;

        public MainPage()
        {
            this.InitializeComponent();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            //base.OnNavigatedTo(e);

            _viewModel = new CurrencyInputViewModel(e.Parameter as List<ICurrency>);

            FirstCurrencyListBox.SelectedItem = _viewModel.CurrencyList.First(c => c.CharCode == "RUB");
            SecondCurrencyListBox.SelectedItem = _viewModel.CurrencyList.First(c => c.CharCode == "USD");

            _viewModel.PropertyChanged += async (sender, eventArgs) => {
                var app = Application.Current as CurrencyMonitorApp;
                var property = eventArgs.PropertyName;

                var fromCurrency = FirstCurrencyListBox.SelectedItem as ICurrency;
                var toCurrency = SecondCurrencyListBox.SelectedItem as ICurrency;

                if (property == nameof(_viewModel.ToCurrencyValue)) {
                    var exchanged = await app.CurrencyExchanger.Exchange(toCurrency, fromCurrency, _viewModel.ToCurrencyValue);
                    _viewModel.FromCurrencyValue = exchanged;
                }
                else {
                    var exchanged = await app.CurrencyExchanger.Exchange(fromCurrency, toCurrency, _viewModel.FromCurrencyValue);
                    _viewModel.ToCurrencyValue = exchanged;
                }
            };

            //this.DataContext = _viewModel;
        }






        //private async void FirstCurrencyValue_OnTextChangedAsync(object sender, TextChangedEventArgs e) {
        //    var app = Application.Current as CurrencyMonitorApp;
        //    var list = await app.CurrencyExchanger.GetCurrencyListAsync();

        //    var context = FirstCurrencyInput.DataContext as CurrencyInputViewModel;

        //    var textBox = sender as TextBox;
        //    decimal.TryParse(textBox.Text, out var value);         
            

        //    var rub = list.First(currency => currency.CharCode == "RUB");
        //    var usd = list.First(currency => currency.CharCode == "USD");

        //    var exchanged = await app.CurrencyExchanger.Exchange(rub, usd, value);
            
        //    (SecondCurrencyInput.DataContext as CurrencyInputViewModel).FromCurrencyValue = exchanged;
        //}

        //private async void SecondCurrencyValue_OnTextChangedAsync(object sender, TextChangedEventArgs e) {
        //    var context = SecondCurrencyInput.DataContext as CurrencyInputViewModel;

        //    var textBox = sender as TextBox;
        //    decimal.TryParse(textBox.Text, out var value);         
            
        //    var app = Application.Current as CurrencyMonitorApp;
        //    var list = await app.CurrencyExchanger.GetCurrencyListAsync();

        //    var rub = list.First(currency => currency.CharCode == "RUB");
        //    var usd = list.First(currency => currency.CharCode == "USD");

        //    var exchanged = await app.CurrencyExchanger.Exchange(usd, rub, value);
            
        //    (FirstCurrencyInput.DataContext as CurrencyInputViewModel).FromCurrencyValue = exchanged;
        //}
    }
}
