using System.Collections.Generic;
using System.Threading.Tasks;
using CurrencyMonitor.Domain.Entities;

namespace CurrencyMonitor.Logic.Services
{
    public interface ICurrencyExchanger
    {
        Task<List<ICurrency>> GetCurrencyListAsync();
        Task<double> Exchange(ICurrency fromCurrency, ICurrency toCurrency, double value);
    }
}
