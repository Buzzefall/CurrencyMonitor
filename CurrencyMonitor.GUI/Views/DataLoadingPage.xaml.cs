using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

using CurrencyMonitor.Domain.Entities;
using CurrencyMonitor.GUI.ViewModels;

namespace CurrencyMonitor.GUI.Views
{
    public sealed partial class DataLoadingPage : Page
    {
        private Task<List<ICurrency>> _dataLoadingTask;

        public DataLoadingPage()
        {
            this.InitializeComponent();
            
            LoadingScreenProgressRing.IsActive = true;

            this.Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs e) {
            var list = await _dataLoadingTask;
            var viewModel = new CurrencyInputViewModel(list);

            viewModel.FromCurrencySelected = viewModel.CurrencyList.First(c => c.CharCode.Equals("RUB"));
            viewModel.ToCurrencySelected = viewModel.CurrencyList.First(c => c.CharCode.Equals("USD"));

            Frame.Navigate(typeof(CurrencyExchangePage), viewModel, new EntranceNavigationTransitionInfo());
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            base.OnNavigatedTo(e);

            var param = e.Parameter;
            var app = Application.Current as CurrencyMonitorApplication;
            _dataLoadingTask = app.CurrencyExchanger.GetCurrencyListAsync();
        }
    }
}
