using System;
using System.Collections.Generic;
using System.Threading;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

using CurrencyMonitor.Domain.Entities;

namespace CurrencyMonitor.GUI.Views
{
    public sealed partial class StartLoadingPage : Page
    {
        List<ICurrency> _currencyList = null;

        public StartLoadingPage()
        {
            this.InitializeComponent();
            
            LoadingScreenProgressRing.IsActive = true;

            this.Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs e) {
            var app = Application.Current as CurrencyMonitorApplication;
            _currencyList = await app.CurrencyExchanger.GetCurrencyListAsync();
            
            Frame.Navigate(typeof(MainPage), _currencyList, new EntranceNavigationTransitionInfo());
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e) {
            base.OnNavigatedTo(e);

        }

    }
}
