using System.Collections.Generic;
using System.Threading.Tasks;
using CurrencyMonitor.Domain.Entities;

namespace CurrencyMonitor.Logic.Services
{
    public interface ICurrencyExchangerService
    {
        Task<List<ICurrency>> GetCurrencyListAsync();
        double Exchange(ICurrency fromCurrency, ICurrency toCurrency, double value);
    }
}
