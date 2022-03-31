using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CurrencyMonitor.GUI.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TestPage2 : Page
    {
        public TestPage2()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        private void Button4_OnClick(object sender, RoutedEventArgs e) {
            ButtonStack.Children.Add(
                new Button() {
                Content = "NEW!"
            });
        }

        private void Button3_OnClick(object sender, RoutedEventArgs e) {
            var app = Application.Current as CurrencyMonitorApplication;
            var task = app.CurrencyExchanger.GetCurrencyListAsync();
            Frame.Navigate(typeof(TestPage1), task);
        }
    }
}
