using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Data.Json;
using CurrencyMonitor.Domain.Entities;
using CurrencyMonitor.Logic.Services.Providers;

namespace CurrencyMonitor.Logic.Services
{
    public class CurrencyExchangerService : ICurrencyExchangerService {
        private List<ICurrency> Currencies { get; set; }
        private ICurrencyDataProvider DataProvider { get; }

        public CurrencyExchangerService(ICurrencyDataProvider provider) {
            DataProvider = provider;
        }

        public async Task<List<ICurrency>> GetCurrencyListAsync() {
            return Currencies ?? (Currencies = await DataProvider.ProvideCurrencyListAsync());
        }

        public double Exchange(ICurrency fromCurrency, ICurrency toCurrency, double value) {
            var fromCurrencyInRubles = fromCurrency.Value / fromCurrency.Nominal;
            var toCurrencyInRubles = toCurrency.Value / toCurrency.Nominal;
            return value * fromCurrencyInRubles / toCurrencyInRubles;
        }
    }
}
