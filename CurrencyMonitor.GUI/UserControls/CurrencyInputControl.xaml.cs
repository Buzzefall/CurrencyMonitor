using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CurrencyMonitor.GUI.UserControls
{
    public sealed partial class CurrencyInputControl : UserControl
    {
        public event RoutedEventHandler ChangeCurrencyButtonClick;

        public static DependencyProperty CurrencyValueProperty { get; set; }
        public static DependencyProperty CurrencyCharCodeProperty { get; set; }

        public double CurrencyValue {
            get => (double)GetValue(CurrencyValueProperty);
            set => SetValue(CurrencyValueProperty, value);
        }

        public string CurrencyCharCode {
            get => (string)GetValue(CurrencyCharCodeProperty);
            set => SetValue(CurrencyCharCodeProperty, value);
        }


        static CurrencyInputControl()
        {
            CurrencyValueProperty = DependencyProperty.Register(
                "CurrencyValue", 
                typeof(double), 
                typeof(CurrencyInputControl), 
                new PropertyMetadata(0.0));

            CurrencyCharCodeProperty = DependencyProperty.Register(
                "CurrencyCharCode",
                typeof(string),
                typeof(CurrencyInputControl),
                new PropertyMetadata("UnknownCurrencyCode"));

        }

        public CurrencyInputControl() {
            this.InitializeComponent();
        }

        private void CurrencyInputTextBox_OnBeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs e) {
            e.Cancel = e.NewText.Any(ch => !char.IsDigit(ch) && !ch.Equals('.')) ||
                       e.NewText.Count(ch => ch.Equals('.')) > 1;
        }

        private void ChangeCurrencyButton_OnClick(object sender, RoutedEventArgs e) {
            ChangeCurrencyButtonClick?.Invoke(this, e);
        }
    }
}
