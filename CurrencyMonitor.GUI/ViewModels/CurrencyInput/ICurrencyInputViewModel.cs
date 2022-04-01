namespace CurrencyMonitor.GUI.ViewModels {
    public interface ICurrencyInputViewModel {
        double FromCurrencyValue { get; set; }
        double ToCurrencyValue { get; set; }
    }
}