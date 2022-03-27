namespace CurrencyMonitor.Logic.Interfaces
{
    public interface ICurrency
    {
        string ID { get; }
        string NumCode { get;}
        string CharCode { get; }
        uint Nominal { get; }
        string Name { get; }
        decimal Value { get;  }
        decimal Previous { get; }
    }
}
