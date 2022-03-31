using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using CurrencyMonitor.Domain.Entities;
using CurrencyMonitor.GUI.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CurrencyMonitor.GUI.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CurrencySelectPage : Page
    {
        private ObservableCollection<ICurrency> _currencyList;

        public CurrencySelectPage()
        {
            this.InitializeComponent();

        }


        private void InitViewModels(NavigationEventArgs e) {
            //_currencySelectorViewModel = new CurrencySelectorViewModel(e.Parameter as List<ICurrency>);

            //_currencySelectorViewModel.FromCurrency = _currencySelectorViewModel.CurrencyList.First(c => c.CharCode.Equals("RUB"));
            //_currencySelectorViewModel.ToCurrency = _currencySelectorViewModel.CurrencyList.First(c => c.CharCode.Equals("USD"));

            //Frame.Navigate;
        }

    }
}
