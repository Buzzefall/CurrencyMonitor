namespace CurrencyMonitor.Domain.Entities
{
    public interface ICurrency
    {
        string ID { get; }
        string NumCode { get;}
        string CharCode { get; }
        uint Nominal { get; }
        string Name { get; }
        double Value { get;  }
        double Previous { get; }
    }
}
