using System.Collections.Generic;
using System.Collections.ObjectModel;
using CurrencyMonitor.App.Annotations;
using CurrencyMonitor.Logic.Interfaces;

namespace CurrencyMonitor.App.ViewModels {
    public interface ICurrencyInputViewModel {
        [NotNull]
        ObservableCollection<ICurrency> CurrencyList { get; set; }
        decimal FromCurrencyValue { get; set; }
        decimal ToCurrencyValue { get; set; }
    }
}