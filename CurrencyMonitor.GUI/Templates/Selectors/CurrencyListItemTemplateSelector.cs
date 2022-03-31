using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using CurrencyMonitor.Domain.Entities;

namespace CurrencyMonitor.GUI.Templates.Selectors {
    public class CurrencyListItemTemplateSelector : DataTemplateSelector {
        protected override DataTemplate SelectTemplateCore(object item) {
            if (item is ICurrency currencyListItem) {
                // ....
            }

            return base.SelectTemplateCore(item);
        }
    }
}