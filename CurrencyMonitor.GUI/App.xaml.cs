using System;

using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using CurrencyMonitor.Logic.Services;
using CurrencyMonitor.GUI.Views;
using CurrencyMonitor.Logic.Services.Providers;

namespace CurrencyMonitor.GUI
{
    sealed partial class CurrencyMonitorApplication : Application {
        public ICurrencyExchangerService CurrencyExchangerService { get; private set; }

        public CurrencyMonitorApplication()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;

            var dataProvider = new CurrencyDataProvider();
            CurrencyExchangerService = new CurrencyExchangerService(dataProvider);
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            var rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null) {
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated) {
                    //TODO: Load state from previously suspended application
                }

                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated != false) return;
            
            if (rootFrame.Content == null) {
                rootFrame.Navigate(typeof(DataLoadingPage), e.Arguments);
            }

            Window.Current.Activate();
        }

        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            //TODO: Save application state and stop any background activity
            
            deferral.Complete();
        }
    }
}
