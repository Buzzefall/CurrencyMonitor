namespace CurrencyMonitor.GUI.ViewModels {
    public interface ICurrencyInputViewModel {
        double FromCurrencySelectedValue { get; set; }
        double ToCurrencySelectedValue { get; set; }
    }
}