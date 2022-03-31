using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Data.Json;
using CurrencyMonitor.Domain.Entities;

namespace CurrencyMonitor.Logic.Services.Providers  {
    public class CurrencyDataProvider : ICurrencyDataProvider {
        private string DataSourceUrl { get; } = "https://www.cbr-xml-daily.ru/daily_json.js";


        public CurrencyDataProvider() { }

        public CurrencyDataProvider(string dataSourceUrl) {
            DataSourceUrl = dataSourceUrl;
        }

        [Pure]
        private static List<ICurrency> ParseJson(string jsonString) {
            JsonObject.TryParse(jsonString, out var jsonObject);

            if (jsonObject == null || !jsonObject.ContainsKey("Valute") || jsonObject["Valute"].GetObject() is null) {
                throw new InvalidDataException("Unexpected structure of currency rates JSON.");
            }

            var currencies = jsonObject["Valute"].GetObject().Values.Select(value => {
                var obj = value.GetObject();
                return new Currency {
                    ID = obj["ID"].GetString(),
                    NumCode = obj["NumCode"].GetString(),
                    CharCode = obj["CharCode"].GetString(),
                    Nominal = (uint) obj["Nominal"].GetNumber(),
                    Name = obj["Name"].GetString(),
                    Value = obj["Value"].GetNumber(),
                    Previous = obj["Previous"].GetNumber(),
                };
            }).ToList<ICurrency>();
            
            return currencies;
        }

        public async Task<List<ICurrency>> ProvideCurrencyListAsync() {
            var client = new Windows.Web.Http.HttpClient();
            var jsonString = await client.GetStringAsync(new Uri(DataSourceUrl));
            
            var currencies = ParseJson(jsonString);
            if (!currencies.Any(currency => currency.CharCode.Equals("RUB", StringComparison.OrdinalIgnoreCase))) {
                currencies.Add(new Currency {
                    ID = "X",
                    CharCode = "RUB",
                    Nominal = 1,
                    Value = 1,
                    NumCode = "X",
                    Name = "Российский рубль",
                    Previous = 1
                });
            }

            return currencies;

        }


    }
}
