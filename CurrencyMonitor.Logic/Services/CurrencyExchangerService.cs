using System.Collections.Generic;
using System.Threading.Tasks;
using CurrencyMonitor.Domain.Entities;
using CurrencyMonitor.Logic.Services.Providers;

namespace CurrencyMonitor.Logic.Services
{
    public class CurrencyExchangerService : ICurrencyExchangerService {
        private List<ICurrency> Currencies { get; set; }
        private ICurrencyListProvider ListProvider { get; }

        public CurrencyExchangerService(ICurrencyListProvider provider) {
            ListProvider = provider;
        }

        public async Task<List<ICurrency>> GetCurrencyListAsync() {
            return Currencies ?? (Currencies = await ListProvider.GetCurrencyListAsync());
        }

        // TODO: rework
        //public double Exchange(ICurrency fromCurrency, ICurrency toCurrency, double value) {
        //    var fromCurrencyInRubles = fromCurrency.Value / fromCurrency.Nominal;
        //    var toCurrencyInRubles =  toCurrency.Value / toCurrency.Nominal;
        //    return  Math.Round(value * fromCurrencyInRubles / toCurrencyInRubles, 2, MidpointRounding.AwayFromZero);
        //}
    }
}
