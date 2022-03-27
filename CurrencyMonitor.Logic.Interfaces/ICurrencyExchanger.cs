using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyMonitor.Logic.Interfaces
{
    public interface ICurrencyExchanger
    {
        Task<List<ICurrency>> GetCurrencyListAsync();
        Task<decimal> Exchange(ICurrency fromCurrency, ICurrency toCurrency, decimal value);
    }
}
