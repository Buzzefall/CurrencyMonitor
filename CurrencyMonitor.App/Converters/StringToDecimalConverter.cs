using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace CurrencyMonitor.App.Converters
{
    public class StringToDecimalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language) {
            decimal.TryParse(value as string, out var result);
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            return ((decimal) value).ToString("F");
        }
    }
}
