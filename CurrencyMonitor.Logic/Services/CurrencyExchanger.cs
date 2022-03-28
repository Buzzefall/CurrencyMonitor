using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Data.Json;
using CurrencyMonitor.Domain;

namespace CurrencyMonitor.Logic.Services
{
    public class CurrencyExchanger : ICurrencyExchanger {
        private readonly string dataSourceUrl;
        private List<ICurrency> _currencies;

        [Pure]
        private static List<ICurrency> ParseJson(string jsonString) {
            JsonObject.TryParse(jsonString, out var jsonObject);
            if (jsonObject == null || !jsonObject.ContainsKey("Valute") || jsonObject["Valute"].GetObject() is null) {
                throw new InvalidDataException("Unexpected structure of currency rates JSON.");
            }

            var currencies = jsonObject["Valute"].GetObject().Values.Select((val) => {
                var obj = val.GetObject();
                var currency = new Currency {
                    ID = obj["ID"].GetString(),
                    NumCode = obj["NumCode"].GetString(),
                    CharCode = obj["CharCode"].GetString(),
                    Nominal = (uint) obj["Nominal"].GetNumber(),
                    Name = obj["Name"].GetString(),
                    Value = obj["Value"].GetNumber(),
                    Previous = obj["Previous"].GetNumber(),
                };
                return currency;
            }).ToList<ICurrency>();
            
            return currencies;
        }


        private async Task<List<ICurrency>> FetchDataAsync() {
            var currencySourceUrl = new Uri(dataSourceUrl);
            var client = new Windows.Web.Http.HttpClient();
            var jsonString = await client.GetStringAsync(currencySourceUrl);
            _currencies = ParseJson(jsonString);
            _currencies.Add(new Currency {
                ID = "-1",
                CharCode = "RUB",
                Nominal = 1,
                Value = 1,
                NumCode = "-1",
                Name = "Российский рубль",
                Previous = 1
            });
            return _currencies;
        }

        public CurrencyExchanger(string dataSourceUrl) {
            this.dataSourceUrl = dataSourceUrl;
        }

        public async Task<List<ICurrency>> GetCurrencyListAsync() {
            return _currencies ?? await FetchDataAsync();
        }

        public async Task<double> Exchange(ICurrency fromCurrency, ICurrency toCurrency, double value) {
            if (_currencies is null) {
                await FetchDataAsync();
            }

            var fromCurrencyInRubles = fromCurrency.Value / fromCurrency.Nominal;
            var toCurrencyInRubles = toCurrency.Value / toCurrency.Nominal;
            return value * fromCurrencyInRubles / toCurrencyInRubles;
        }
    }
}
