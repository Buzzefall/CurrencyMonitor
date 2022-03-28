using System;
using Windows.UI.Xaml.Data;

namespace CurrencyMonitor.App.Converters
{
    public class DecimalToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language) {
            return ((decimal) value).ToString("F");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            decimal.TryParse(value as string, out var result);
            return result;
        }
    }
}
